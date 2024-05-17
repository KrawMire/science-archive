/**
 * Represents user data
 */
export type User = {
  id?: string;

  /**
   * User name
   */
  name: string;

  /**
   * User email
   */
  email: string;

  /**
   * Login of user
   */
  login: string;

  isConfirmed: boolean;
  articles: UserArticle[];
  about?: string;
}

export type UserArticle = {
  articleId: string;
  title: string;
} ;
