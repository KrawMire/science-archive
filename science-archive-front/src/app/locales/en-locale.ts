import { en_US } from "ng-zorro-antd/i18n";

export const enLocale = {
  en_US,
  ...{
    commonErrors: {
      unhandledError: "Unhandled error occurred",
      articleIdNotPresent: "Article ID is not present"
    },
    contentPage: {
      headerMenu: {
        articles: "Articles",
        categories: "Categories",
        news: "News",
        signIn: "Sign In"
      },
      footer: {
        contactUs: "Contact Us",
        telegramChannel: "Telegram Channel",
        menu: "Menu",
        articles: "Articles",
        categories: "Categories",
        news: "News",
      },
      accountDrawer: {
        myProfile: "My Profile",
        myArticles: "My Articles",
        adminPanel: "Admin Panel",
        signOut: "Sign Out",
      }
    },
    welcomePage: {
      goalText: "Our mission is to give to independent scientists and other explorers ability to publish their works or preprints and share it with the world.",
      goToArticles: "Go to Articles",
    },
    authPage: {
      messages: {
        verificationCodeSent: "Verification code was send to your email"
      },
      errors: {
        userNotPresent: "User is not present",
        codeWasResentManyTimes: "Code was resent over 5 times",
        invalidCode: "Code is invalid",
      },
      goalText: "Our mission is to give to independent scientists and other explorers ability to publish their works or preprints and share it with the world.",
      signInForm: {
        signIn: "Sign in",
        inputValidLoginOrEmail: "Please input valid login or email!",
        inputPassword: "Please input your password!",
        loginOrEmail: "Login or email",
        password: "Password",
        logIn: "Log in",
        dontHaveAccount: "Don't have an account?",
        signUp: "Sign up",
      },
      signUpForm: {
        signUp: "Sign up",
        nameMustContain: "Name must contain al least 2 letters!",
        loginMustContain: "Login must contain at least 3 letters!",
        inputEmail: "Please input your real email!",
        passwordMustContain: "Password must contain at least 10 symbols!",
        repeatedPasswordMustBeEqual: "Repeated password should be equal to password!",
        firstName: "First name",
        secondName: "Second name",
        login: "Login",
        email: "Email",
        password: "Password",
        repeatPassword: "Repeat password",
        register: "Register",
        alreadyHaveAnAccount: "Already have an account?",
        signIn: "Sign in"
      },
      confirmForm: {
        enterCode: "Enter confirmation code",
        resendCode: "Resend code",
        confirm: "Confirm",
        confirmCode: "Confirm code",
      }
    },
    articlesPage: {
      articles: "Articles",
      noArticlesFound: "No articles found",
      readAllArticles: "Read all articles",
    },
    articlePage: {
      articles: "Articles",
      linkedDocuments: "Linked documents",
      noDocuments: "No documents",
      backToArticles: "Back to articles",
      invalidSearchData: "Invalid search data",
      incorrectDataWasPassed: "Incorrect search data was passed",
      forbiddenResource: "Forbidden resource",
      youCannotView: "You cannot view this article",
      notFound: "404",
      articleNotExist: "This article does not exist",
      serverError: "Server error",
      internalServerError: "Internal server error",
      unknownError: "Unknown error",
      unableToProcess: "We were unable to process your request"
    },
    categoriesPage: {
      noCategoriesFound: "No categories found"
    },
    newsCard: {
      readMore: "Read more"
    },
    newsDetailsPage: {
      news: "News",
      invalidSearchData: "Invalid search data",
      incorrectDataWasPassed: "Incorrect search data was passed",
      notFound: "404",
      newsNotExist: "This news does not exist",
      serverError: "Server error",
      internalServerError: "Internal server error",
      unknownError: "Unknown error",
      unableToProcess: "We were unable to process your request"
    },
    newsPage: {
      backToNews: "Back to news"
    },
    accountPage: {
      account: "Account",
      profile: "Profile",
      myArticles: "My articles",
      allArticles: "All articles",
      backToMain: "Back to main",
    },
    profilePage: {
      profile: "Profile",
      name: "Name",
      login: "Login",
      email: "Email",
      confirmed: "Confirmed"
    },
    userArticleCard: {
      onProcessing: "On processing",
      published: "Published",
      declined: "Declined"
    },
    userArticlesPage: {
      youHaveNotCreatedArticle: "You haven't created any article",
      createFirstArticle: "Create your first article",
      allArticles: "All articles",
      new: "New",
      createNewArticle: "Create new article",
      title: "Title",
      category: "Category",
      subcategory: "Subcategory",
      description: "Description",
      dragDocsToArea: "Click or drag documents to this area to upload",
      supportSingleUpload: "Support only single upload. Strictly prohibit from uploading company data or other band files",
      specifyArticleTitle: "Please specify article title",
      specifyArticleCategory: "Please specify article category",
      specifyArticleSubcategory: "Please specify article subcategory",
      specifyDescription: "Please input article description",
      articleTitle: "Article title",
      describeArticle: "Describe what about your article is...",
      onlyForAuthorized: "This action is only for authorized users",
      articleCreated: "Article was successfully created",
      documentUploaded: "Document was successfully uploaded",
      cannotUploadDocument: "An error occurred while uploading document"
    },
    adminPage: {
      administration: "Administration",
      articles: "Articles",
      news: "News",
      backToMain: "Back to main"
    },
    adminArticleCard: {
      approve: "Approve",
      decline: "Decline",
      onProcessing: "On processing",
      published: "Published",
      declined: "Declined",
      sureApproval: "Are you sure about this article approval?",
      sureDeviation: "Are you sure about this article deviation?",
      cannotBeUndone: "This cannot be undone",
      articleApproved: "Article was successfully approved",
      articleDeclined: "Article was successfully declined"
    },
    adminArticlesPage: {
      noArticles: "No articles",
      allArticles: "All articles"
    },
    adminNewsPage: {
      noNews: "No news",
      news: "News",
      new: "New",
      createNewArticle: "Create new article",
      specifyNewsTitle: "Please specify news title",
      specifyNewsBody: "Please input news body",
      newsTitle: "News title",
      newsBody: "News body...",
      title: "Title",
      body: "Body"
    }
  }
}
