import { ru_RU } from "ng-zorro-antd/i18n";
import { enLocale } from "./en-locale";

export const ruLocale = {
  ru_RU,
  ...enLocale,
  ...{
    commonErrors: {
      unhandledError: "Произошла непредвиденная ошибка",
      articleIdNotPresent: "Не удалось получить ID статьи"
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
    },
    accountPage: {
      account: "Аккаунт",
      profile: "Профиль",
      myArticles: "Мои статьи",
      allArticles: "Все статьи",
      backToMain: "Назад на главную",
    },
    profilePage: {
      profile: "Профиль",
      name: "Имя",
      login: "Логин",
      email: "Email",
      confirmed: "Подтвержден"
    },
    userArticleCard: {
      onProcessing: "На проверке",
      published: "Опубликовано",
      declined: "Отклонено"
    },
    userArticlesPage: {
      youHaveNotCreatedArticle: "Вы еще не создали ни одной статьи",
      createFirstArticle: "Создайте свою первую статью",
      allArticles: "Все статьи",
      new: "Новая статья",
      createNewArticle: "Создание новой статьи",
      title: "Заголовок",
      category: "Категория",
      subcategory: "Подкатегория",
      description: "Описание",
      dragDocsToArea: "Кликните или перенесите документы в эту область для загрузки",
      supportSingleUpload: "Поддерживает только загрузку по одному файлу. Строго не рекомендуем загружать конфиденциальные файлы",
      specifyArticleTitle: "Укажите заголовок статьи",
      specifyArticleCategory: "Укажите категорию статьи",
      specifyArticleSubcategory: "Укажите подкатегорию статьи",
      specifyDescription: "Укажите описание статьи",
      articleTitle: "Заголовок статьи",
      describeArticle: "Опишите о чем ваша статья...",
      onlyForAuthorized: "Это действие доступно только для авторизованных пользователей",
      articleCreated: "Статья успешно создана",
      documentUploaded: "Документ был успешно загружен",
      cannotUploadDocument: "Возникла ошибка во время загрузки документа"
    },
    adminPage: {
      administration: "Панель администратора",
      articles: "Статьи",
      news: "Новости",
      backToMain: "Назад на главную"
    },
    adminArticleCard: {
      approve: "Одобрить",
      decline: "Отклонить",
      onProcessing: "На проверке",
      published: "Опубликовано",
      declined: "Отклонено",
      sureApproval: "Вы уверены, что хотите одобрить эту статью?",
      sureDeviation: "Вы уверены, что хотите отклонить эту статью?",
      cannotBeUndone: "Это действие не может быть отменено",
      articleApproved: "Статья успешно одобрена",
      articleDeclined: "Статья успешно отклонена"
    },
    adminArticlesPage: {
      noArticles: "Нет статей на проверку",
      allArticles: "Все статьи"
    },
    adminNewsPage: {
      noNews: "Нет новостей",
      news: "Новости",
      new: "Новая новость",
      createNewArticle: "Создание новой новости",
      specifyNewsTitle: "Укажите заголовок новости",
      specifyNewsBody: "Укажите тело новости",
      newsTitle: "Заголовок новости",
      newsBody: "Тело новости...",
      title: "Заголовок",
      body: "Тело"
    }
  }
}
