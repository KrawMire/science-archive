import { ru_RU } from "ng-zorro-antd/i18n";
import { enLocale } from "./en-locale";

export const ruLocale = {
  ru_RU,
  ...enLocale,
  ...{
    commonErrors: {
      unhandledError: "Произошла непредвиденная ошибка"
    },
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
    welcomePage: {
      goalText: "Наша миссия - предоставить независимым ученым и другим исследователям возможность публиковать свои работы и делиться ими со всем миром. В наши дни доступ к современной научной литературе обходится очень дорого. Так же как и публикация в любом уважаемом издательстве.",
      goToArticles: "Перейти к статьям",
    },
    authPage: {
      messages: {
        verificationCodeSent: "Код подтверждения был отправлен на ваш адрес вашей электронной почты"
      },
      errors: {
        userNotPresent: "Пользователь не найден",
        codeWasResentManyTimes: "Код был запрошен более 5 раз",
        invalidCode: "Неверный код",
      },
      goalText: "Наша миссия - предоставить независимым ученым и другим исследователям возможность публиковать свои работы и делиться ими со всем миром. В наши дни доступ к современной научной литературе обходится очень дорого. Так же как и публикация в любом уважаемом издательстве.",
      signInForm: {
        signIn: "Вход",
        inputValidLoginOrEmail: "Введите логин или почту",
        inputPassword: "Введите пароль!",
        loginOrEmail: "Логин или email-адрес",
        password: "Пароль",
        logIn: "Войти",
        dontHaveAccount: "Нет учетной записи?",
        signUp: "Зарегистрироваться",
      },
      signUpForm: {
        signUp: "Регистрация",
        nameMustContain: "Имя должно содержать хотя бы 2 буквы!",
        loginMustContain: "Логин должен содержать хотя бы 3 буквы!",
        inputEmail: "Укажите корректный email!",
        passwordMustContain: "Пароль должен содержать минимум 10 символов!",
        repeatedPasswordMustBeEqual: "Повтор пароля должен совпадать с паролем",
        firstName: "Фамилия",
        secondName: "Имя",
        login: "Логин",
        email: "Электронная почта",
        password: "Пароль",
        repeatPassword: "Повтор пароля",
        register: "Зарегистрироваться",
        alreadyHaveAnAccount: "Уже есть учетная запись?",
        signIn: "Войти"
      },
      confirmForm: {
        enterCode: "Введите код подтверждения",
        resendCode: "Отправить код еще раз",
        confirm: "Подтвердить",
        confirmCode: "Код подтверждения"
      }
    },
    articlesPage: {
      articles: "Статьи",
      noArticlesFound: "Ни одной статьи не найдено",
      readAllArticles: "Перейти ко всем статьям",
    },
    articlePage: {
      articles: "Статьи",
      linkedDocuments: "Прикрепленные документы",
      noDocuments: "Прикрепленных документов нет",
      backToArticles: "Назад к статьям",
      invalidSearchData: "Некорректные поисковые данные",
      incorrectDataWasPassed: "Переданы некорректные данные для поиска",
      forbiddenResource: "Доступ ограничен",
      youCannotView: "Вы не можете просматривать эту статью",
      notFound: "404",
      articleNotExist: "Статья не существует",
      serverError: "Серверная ошибка",
      internalServerError: "Внутренняя серверная ошибка",
      unknownError: "Неизвестная ошибка",
      unableToProcess: "Мы не смогли обработать ваш запрос"
    },
    categoriesPage: {
      noCategoriesFound: "Никаких категорий не найдено"
    },
    newsCard: {
      readMore: "Читать далее"
    },
    newsDetailsPage: {
      news: "Новости",
      invalidSearchData: "Неверный формат данных для поиска",
      incorrectDataWasPassed: "Неверные поисковые данные были переданы",
      notFound: "404",
      newsNotExist: "Такой новости нет",
      serverError: "Серверная ошибка",
      internalServerError: "Внутренная серверная ошибка",
      unknownError: "Неизвестная ошибка",
      unableToProcess: "Мы не смогли обработать ваш запрос"
    },
    newsPage: {
      backToNews: "Назад к новостям"
    }
  }
}
