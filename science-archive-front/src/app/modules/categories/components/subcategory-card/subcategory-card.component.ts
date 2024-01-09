import { Component, Input } from "@angular/core";

@Component({
  selector: "sar-subcategory-card",
  templateUrl: "./subcategory-card.component.html",
  styleUrls: ["./subcategory-card.component.scss"],
})
export class SubcategoryCardComponent {
  @Input() style?: string;
}
