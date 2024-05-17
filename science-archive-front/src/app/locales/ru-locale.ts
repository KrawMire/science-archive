import { ru_RU } from "ng-zorro-antd/i18n";
import { enLocale } from "./en-locale";

export const ruLocale = {
  ru_RU,
  ...enLocale,
  ...{
    ContentPage: {
      ArticlesMenuElement: "Статьи",
      CategoriesMenuElement: "Категории",
      NewsMenuElement: "Новости",
    },
    ArticlesPage: {

    }
  }
}
