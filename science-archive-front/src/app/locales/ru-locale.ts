import { ru_RU } from "ng-zorro-antd/i18n";
import { enLocale } from "./en-locale";

export const ruLocale = {
  ru_RU,
  ...enLocale,
  ...{
    contentPage: {
      headerMenu: {
        articles: "Статьи",
        categories: "Категории",
        news: "Новости",
        signIn: "Войти"
      },
      footer: {
        contactUs: "Связаться с нами",
        telegramChannel: "Телеграм канал",
        menu: "Меню",
        articles: "Статьи",
        categories: "Категории",
        news: "Новости",
      },
      accountDrawer: {
        myProfile: "Мой профиль",
        myArticles: "Мои статьи",
        adminPanel: "Панель администратора",
        signOut: "Выйти",
      }
    },
    articlesPage: {

    }
  }
}
