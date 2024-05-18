import { Component, EventEmitter, Input, Output } from "@angular/core";
import { Article } from "@models/article/article";

@Component({
  selector: "app-admin-article-card",
  templateUrl: "./admin-article-card.component.html",
  styleUrls: ["./admin-article-card.component.scss"],
})
export class AdminArticleCardComponent {
  @Input() article!: Article;
  @Output() articleSelect = new EventEmitter<string>();

  onClick() {
    this.articleSelect.emit(this.article.id);
  }
}
