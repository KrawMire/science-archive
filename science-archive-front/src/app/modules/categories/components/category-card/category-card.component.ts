import { Component, Input } from "@angular/core";

@Component({
  selector: "sar-category-card",
  templateUrl: "./category-card.component.html",
  styleUrls: ["./category-card.component.scss"],
})
export class CategoryCardComponent {
  @Input() style?: string;
}
