import { Component, Input } from "@angular/core";
import { Subcategory } from "@models/category/subcategory";

@Component({
  selector: "app-subcategory-card",
  templateUrl: "./subcategory-card.component.html",
  styleUrls: ["./subcategory-card.component.scss"],
})
export class SubcategoryCardComponent {
  @Input() subcategory: Subcategory | null = null;
}
