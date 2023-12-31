import { Article } from "@models/article/article";

export interface UpdateArticleRequest {
  id: string;
  article: Article;
}
