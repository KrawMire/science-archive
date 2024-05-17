import { en_US } from "ng-zorro-antd/i18n";

export const enLocale = {
  en_US,
  ...{
    commonErrors: {
      unhandledError: "Unhandled error occurred"
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
      goalText: "Our mission is to give to independent scientists and other explorers ability to publish their works and share it with the world. It's so expensive to get access to modern science literature in nowadays. So is to achieve publishing in any reputable publishing house.",
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
      goalText: "Our mission is to give to independent scientists and other explorers ability to publish their works and share it with the world. It's so expensive to get access to modern science literature in nowadays. So is to achieve publishing in any reputable publishing house.",
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
    }
  }
}
