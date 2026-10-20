import { Component, OnDestroy, OnInit } from "@angular/core";
import { AsyncPipe, NgForOf, NgIf } from "@angular/common";
import { RouterLink } from "@angular/router";
import { Title } from "@angular/platform-browser";
import { Subscription } from "rxjs";
import { NzMessageService } from "ng-zorro-antd/message";
import { NzModalService } from "ng-zorro-antd/modal";
import { NzI18nPipe, NzI18nService } from "ng-zorro-antd/i18n";
import { Article } from "@models/article/article";
import { ArticleApiService } from "@services/article-api.service";
import { SharedModule } from "@modules/shared/shared.module";
import { ModerationStoreService } from "@modules/admin/services/moderation-store.service";
import { ArticleStatus, authorNames } from "@modules/shared/utils/article-utils";

@Component({
  selector: 'sar-admin-articles-page',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    AsyncPipe,
    RouterLink,
    SharedModule,
    NzI18nPipe
  ],
  templateUrl: './admin-articles-page.component.html',
  styleUrl: './admin-articles-page.component.scss'
})
export class AdminArticlesPageComponent implements OnInit, OnDestroy {
  readonly ArticleStatus = ArticleStatus;
  readonly tabs: { status: ArticleStatus, label: string, description: string }[] = [
    { status: ArticleStatus.ToVerify, label: "status.toVerify", description: "admin.toVerifyDescription" },
    { status: ArticleStatus.Verified, label: "status.verified", description: "admin.verifiedDescription" },
    { status: ArticleStatus.Declined, label: "status.declined", description: "admin.declinedDescription" },
  ];

  readonly isLoading$ = this.moderationStore.isLoading$;
  readonly hasError$ = this.moderationStore.hasError$;

  status: ArticleStatus = ArticleStatus.ToVerify;
  queue: Article[] = [];
  selected?: Article;
  isProcessing = false;

  private allArticles: Article[] = [];
  private readonly subscription = new Subscription();

  constructor(
    private readonly i18nService: NzI18nService,
    private readonly messageService: NzMessageService,
    private readonly modalService: NzModalService,
    private readonly articleService: ArticleApiService,
    private readonly moderationStore: ModerationStoreService,
    titleService: Title,
  ) {
    titleService.setTitle("Science Archive - Moderation");
  }

  ngOnInit(): void {
    this.subscription.add(
      this.moderationStore.articles$.subscribe(articles => {
        this.allArticles = articles;
        this.updateQueue();
      })
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  get description(): string {
    return this.tabs.find(tab => tab.status === this.status)?.description ?? "";
  }

  setStatus(status: ArticleStatus): void {
    this.status = status;
    this.selected = undefined;
    this.updateQueue();
  }

  select(article: Article): void {
    this.selected = article;
  }

  authorsOf(article: Article): string {
    return authorNames(article);
  }

  skip(): void {
    if (!this.selected || this.queue.length < 2) {
      return;
    }

    const index = this.queue.findIndex(article => article.id === this.selected?.id);
    this.selected = this.queue[(index + 1) % this.queue.length];
  }

  onApprove(article: Article): void {
    this.modalService.confirm({
      nzTitle: this.i18nService.translate("adminArticleCard.sureApproval"),
      nzContent: this.i18nService.translate("adminArticleCard.cannotBeUndone"),
      nzOnOk: () => this.moderate(article, ArticleStatus.Verified)
    });
  }

  onDecline(article: Article): void {
    this.modalService.confirm({
      nzTitle: this.i18nService.translate("adminArticleCard.sureDeviation"),
      nzContent: this.i18nService.translate("adminArticleCard.cannotBeUndone"),
      nzOkDanger: true,
      nzOnOk: () => this.moderate(article, ArticleStatus.Declined)
    });
  }

  private moderate(article: Article, status: ArticleStatus.Verified | ArticleStatus.Declined): void {
    if (!article.id) {
      this.messageService.error(this.i18nService.translate("commonErrors.articleIdNotPresent"));
      return;
    }

    const request = status === ArticleStatus.Verified
      ? this.articleService.approveArticle(article.id)
      : this.articleService.declineArticle(article.id);

    // Move on to the next article in the queue once this one leaves it
    const index = this.queue.findIndex(item => item.id === article.id);
    const next = this.queue[index + 1] ?? this.queue[index - 1];

    this.isProcessing = true;
    request.subscribe({
      next: () => {
        this.isProcessing = false;
        this.messageService.success(this.i18nService.translate(
          status === ArticleStatus.Verified ? "adminArticleCard.articleApproved" : "adminArticleCard.articleDeclined"
        ));
        this.selected = next;
        this.moderationStore.refresh();
      },
      error: (error) => {
        this.isProcessing = false;
        this.messageService.error(error.message ?? error);
      }
    });
  }

  private updateQueue(): void {
    this.queue = this.allArticles.filter(article => article.status === this.status);
    this.selected = this.queue.find(article => article.id === this.selected?.id) ?? this.queue[0];
  }
}
