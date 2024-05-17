import { Component, Input } from "@angular/core";

@Component({
  selector: "sar-category-card-skeleton",
  templateUrl: "./category-card-skeleton.component.html",
  styleUrls: ["./category-card-skeleton.component.scss"],
})
export class CategoryCardSkeletonComponent {
  @Input() style?: string;
}
