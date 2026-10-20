import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Params, Router } from "@angular/router";
import { Title } from "@angular/platform-browser";
import { BehaviorSubject, debounceTime, forkJoin, Subject, Subscription } from "rxjs";
import { NzI18nService } from "ng-zorro-antd/i18n";
import { Article } from "@models/article/article";
import { Category } from "@models/category/category";
import { Subcategory } from "@models/category/subcategory";
import { ArticleApiService } from "@services/article-api.service";
import { CategoryApiService } from "@services/category-api.service";
import { authorNames, findField, hasDocumentOfKind, toDate } from "@modules/shared/utils/article-utils";

export type ArticleSort = "newest" | "oldest" | "title";

type ArticleFilters = {
  categoryId: string | null;
  fieldId: string | null;
  query: string;
  withPdf: boolean;
  withData: boolean;
  sort: ArticleSort;
  page: number;
};

const PAGE_SIZE = 10;

@Component({
  selector: "sar-articles-page",
  templateUrl: "./articles-page.component.html",
  styleUrls: ["./articles-page.component.scss"]
})
export class ArticlesPageComponent implements OnInit, OnDestroy {
  isLoading$ = new BehaviorSubject<boolean>(true);
  hasError = false;
  showFilters = false;

  categories: Category[] = [];
  filters: ArticleFilters = this.parseFilters({});
  searchText = "";

  activeField?: Category;
  activeCategory?: Subcategory;
  pageArticles: Article[] = [];
  totalResults = 0;
  pages: number[] = [];

  private allArticles: Article[] = [];
  private readonly searchInput$ = new Subject<string>();
  private readonly subscriptions = new Subscription();

  constructor(
    private readonly i18nService: NzI18nService,
    private readonly articleService: ArticleApiService,
    private readonly categoryService: CategoryApiService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly titleService: Title) {}

  ngOnInit(): void {
    this.subscriptions.add(
      forkJoin({
        articles: this.articleService.getAllVerifiedArticles(),
        categories: this.categoryService.getAllCategories(),
      }).subscribe({
        next: ({ articles, categories }) => {
          this.allArticles = articles.articles;
          this.categories = categories.categories;
          this.isLoading$.next(false);
          this.applyFilters();
        },
        error: (error) => {
          console.log(error);
          this.hasError = true;
          this.isLoading$.next(false);
        },
      })
    );

    this.subscriptions.add(
      this.route.queryParams.subscribe(params => {
        this.filters = this.parseFilters(params);
        this.searchText = this.filters.query;
        this.applyFilters();
      })
    );

    this.subscriptions.add(
      this.searchInput$
        .pipe(debounceTime(300))
        .subscribe(query => this.updateFilters({ q: query.trim() || null }, true))
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  get pageTitle(): string {
    if (this.activeCategory) {
      return this.activeCategory.name;
    }

    if (this.activeField) {
      return this.activeField.name;
    }

    return this.i18nService.translate("articles.allArticles");
  }

  get hasActiveFilters(): boolean {
    const f = this.filters;
    return !!(f.categoryId || f.fieldId || f.query || f.withPdf || f.withData);
  }

  fieldNameOf(article: Article): string | undefined {
    return findField(this.categories, article.categoryId)?.name;
  }

  isFieldExpanded(field: Category): boolean {
    return this.activeField?.id === field.id;
  }

  onSearchInput(value: string): void {
    this.searchInput$.next(value);
  }

  onSearchSubmit(): void {
    this.updateFilters({ q: this.searchText.trim() || null });
  }

  selectAll(): void {
    this.updateFilters({ categoryId: null, fieldId: null });
  }

  selectField(field: Category): void {
    this.updateFilters({ fieldId: field.id, categoryId: null });
  }

  selectCategory(category: Subcategory): void {
    this.updateFilters({ categoryId: category.id, fieldId: null });
  }

  setDocumentFilter(name: "pdf" | "data", enabled: boolean): void {
    this.updateFilters({ [name]: enabled ? "1" : null });
  }

  setSort(sort: string): void {
    this.updateFilters({ sort: sort === "newest" ? null : sort });
  }

  clearQuery(): void {
    this.searchText = "";
    this.updateFilters({ q: null });
  }

  clearAll(): void {
    this.searchText = "";
    this.router.navigate([], { relativeTo: this.route, queryParams: {} });
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.pages.length || page === this.filters.page) {
      return;
    }

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { page: page === 1 ? null : page },
      queryParamsHandling: "merge",
    });
  }

  private updateFilters(params: Params, replaceUrl = false): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { ...params, page: null },
      queryParamsHandling: "merge",
      replaceUrl,
    });
  }

  private parseFilters(params: Params): ArticleFilters {
    const sort = params["sort"];
    const page = Number(params["page"]);

    return {
      categoryId: params["categoryId"] || null,
      fieldId: params["fieldId"] || null,
      query: params["q"] || "",
      withPdf: params["pdf"] === "1",
      withData: params["data"] === "1",
      sort: sort === "oldest" || sort === "title" ? sort : "newest",
      page: Number.isInteger(page) && page > 0 ? page : 1,
    };
  }

  private applyFilters(): void {
    const f = this.filters;

    this.activeCategory = undefined;
    this.activeField = undefined;

    if (f.categoryId) {
      this.activeField = findField(this.categories, f.categoryId);
      this.activeCategory = this.activeField?.subcategories.find(sub => sub.id === f.categoryId);
    } else if (f.fieldId) {
      this.activeField = this.categories.find(category => category.id === f.fieldId);
    }

    const fieldCategoryIds = this.activeField && !f.categoryId
      ? new Set(this.activeField.subcategories.map(sub => sub.id))
      : null;
    const query = f.query.toLowerCase();

    const filtered = this.allArticles
      .filter(article => !f.categoryId || article.categoryId === f.categoryId)
      .filter(article => !fieldCategoryIds || fieldCategoryIds.has(article.categoryId))
      .filter(article => !f.withPdf || hasDocumentOfKind(article, "pdf"))
      .filter(article => !f.withData || hasDocumentOfKind(article, "data"))
      .filter(article => !query || this.matchesQuery(article, query))
      .sort((a, b) => this.compare(a, b, f.sort));

    const pageCount = Math.max(1, Math.ceil(filtered.length / PAGE_SIZE));
    const page = Math.min(f.page, pageCount);

    this.totalResults = filtered.length;
    this.pages = Array.from({ length: pageCount }, (_, index) => index + 1);
    this.filters = { ...f, page };
    this.pageArticles = filtered.slice((page - 1) * PAGE_SIZE, page * PAGE_SIZE);

    this.titleService.setTitle(`Science Archive - ${this.pageTitle}`);
  }

  private matchesQuery(article: Article, query: string): boolean {
    return [article.title, article.description ?? "", authorNames(article), article.categoryName]
      .some(text => text.toLowerCase().includes(query));
  }

  private compare(a: Article, b: Article, sort: ArticleSort): number {
    if (sort === "title") {
      return a.title.localeCompare(b.title);
    }

    const difference = (toDate(a.creationDate)?.getTime() ?? 0) - (toDate(b.creationDate)?.getTime() ?? 0);
    return sort === "oldest" ? difference : -difference;
  }
}
