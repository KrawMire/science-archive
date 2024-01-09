import { Component, Input } from "@angular/core";

@Component({
  selector: "sar-news-card",
  templateUrl: "./news-card.component.html",
  styleUrls: ["./news-card.component.scss"],
})
export class NewsCardComponent {
  @Input() style?: string;

  date = new Date();
}
