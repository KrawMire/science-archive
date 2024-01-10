import { Component, Input } from "@angular/core";

@Component({
  selector: "sar-news-card-skeleton",
  templateUrl: "./news-card-skeleton.component.html",
  styleUrls: ["./news-card-skeleton.component.scss"],
})
export class NewsCardSkeletonComponent {
  @Input() style?: string;
}
