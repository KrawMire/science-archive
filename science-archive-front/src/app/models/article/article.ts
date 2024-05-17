/**
 * Article data
 */
export type Article = {
  /**
   * Identifier of the article
   */
  id?: string;

  /**
   * The name of the category.
   */
  categoryName: string;

  /**
   * ID of category
   */
  categoryId: string;

  /**
   * Article title
   */
  title: string;

  /**
   * Identifier of the user
   * created the article
   */
  authors: ArticleAuthor[];

  /**
   * Date when article was created
   */
  creationDate?: Date;

  /**
   * Article description
   */
  description: string | null;

  /**
   * Paths to documents linked to article
   */
  documents: ArticleDocument[];

  /**
   * Current article status represented as number
   */
  status: number;
};

export type ArticleDocument = {
  id?: string;
  name: string;
  path: string;
};

export type ArticleAuthor = {
  userId: string;
  name: string;
  role: number;
}
