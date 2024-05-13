import { Subcategory } from "@models/category/subcategory";

export type Category = {
  id: string;
  name: string;
  description?: string;
  subcategories: Subcategory[];
}
