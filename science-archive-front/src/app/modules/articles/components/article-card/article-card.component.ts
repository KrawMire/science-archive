import { Component, Input } from "@angular/core";

@Component({
  selector: "sar-article-card",
  templateUrl: "./article-card.component.html",
  styleUrls: ["./article-card.component.scss"],
})
export class ArticleCardComponent {
  @Input() style?: string;
}
