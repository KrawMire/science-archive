import { Component, Input } from "@angular/core";

@Component({
  selector: "sar-article-card-skeleton",
  templateUrl: "./article-card-skeleton.component.html",
  styleUrls: ["./article-card-skeleton.component.scss"],
})
export class ArticleCardSkeletonComponent {
  @Input() style?: string;
}
