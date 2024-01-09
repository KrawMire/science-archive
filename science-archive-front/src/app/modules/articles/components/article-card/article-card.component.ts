import { Component, Input } from "@angular/core";
import { Article } from "@models/article/article";

@Component({
  selector: "sar-article-card",
  templateUrl: "./article-card.component.html",
  styleUrls: ["./article-card.component.scss"],
})
export class ArticleCardComponent {
  @Input() style?: string;
  @Input() article!: Article;
}
