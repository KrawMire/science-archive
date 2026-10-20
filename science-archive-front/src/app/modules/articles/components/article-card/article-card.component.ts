import { ChangeDetectionStrategy, Component, Input } from "@angular/core";
import { Article } from "@models/article/article";
import { authorNames, isNewArticle } from "@modules/shared/utils/article-utils";

export type ArticleCardVariant = "grid" | "list" | "compact";

@Component({
  selector: "sar-article-card",
  templateUrl: "./article-card.component.html",
  styleUrls: ["./article-card.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ArticleCardComponent {
  @Input({ required: true }) article!: Article;
  @Input() variant: ArticleCardVariant = "list";

  /**
   * Name of the field (top-level category) the article belongs to, shown next to its category
   */
  @Input() fieldName?: string;

  get authors(): string {
    return authorNames(this.article);
  }

  get isNew(): boolean {
    return isNewArticle(this.article);
  }

  get link(): string[] {
    return ["/content", "articles", this.article.id ?? ""];
  }
}
