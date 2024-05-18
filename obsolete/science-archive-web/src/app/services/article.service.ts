import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Response } from "@models/common/response";
import { GetArticleByIdResponse } from "@models/article/responses/get-article-by-id.response";
import { GetAllArticlesResponse } from "@models/article/responses/get-all-articles.response";
import { GetArticlesByCategoryIdResponse } from "@models/article/responses/get-articles-by-category-id.response";
import { ApiService } from "@services/api.service";
import { GetArticlesByAuthorIdResponse } from "@models/article/responses/get-articles-by-author-id.response";
import { Article } from "@models/article/article";
import { CreateArticleResponse } from "@models/article/responses/create-article.response";
import { CreateArticleRequest } from "@models/article/requests/create-article.request";
import { UpdateArticleResponse } from "@models/article/responses/update-article.response";
import { UpdateArticleRequest } from "@models/article/requests/update-article.request";
import { DeleteArticleResponse } from "@models/article/responses/delete-article.response";
import { ApproveArticleResponse } from "@models/article/responses/approve-article.response";
import { DeclineArticleResponse } from "@models/article/responses/decline-article.response";
import { ApproveArticleRequest } from "@models/article/requests/approve-article.request";
import { DeclineArticleRequest } from "@models/article/requests/decline-article.request";

@Injectable({
  providedIn: "root",
})
export class ArticleService extends ApiService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  getAllArticles(): Observable<GetAllArticlesResponse> {
    const response = this.httpClient.get<Response<GetAllArticlesResponse>>("api/articles/all");
    return this.handleResponse(response);
  }

  getAllVerifiedArticles(): Observable<GetAllArticlesResponse> {
    const response = this.httpClient.get<Response<GetAllArticlesResponse>>("/api/articles/");
    return this.handleResponse(response);
  }

  getArticlesByCategoryId(categoryId: string): Observable<GetArticlesByCategoryIdResponse> {
    const response = this.httpClient.get<Response<GetArticlesByCategoryIdResponse>>(
      `/api/articles/by-category/${categoryId}`
    );
    return this.handleResponse(response);
  }

  getArticleByAuthorId(authorId: string): Observable<GetArticlesByAuthorIdResponse> {
    const response = this.httpClient.get<Response<GetArticlesByAuthorIdResponse>>(
      `/api/articles/by-author/${authorId}`
    );
    return this.handleResponse(response);
  }

  getMyArticles(): Observable<GetArticlesByAuthorIdResponse> {
    const response = this.httpClient.get<Response<GetArticlesByAuthorIdResponse>>(`/api/articles/my-articles`);
    return this.handleResponse(response);
  }

  getArticleById(id: string): Observable<GetArticleByIdResponse> {
    const response = this.httpClient.get<Response<GetArticleByIdResponse>>(`/api/articles/${id}`);
    return this.handleResponse(response);
  }

  createArticle(newArticle: Article): Observable<CreateArticleResponse> {
    const dto: CreateArticleRequest = {
      article: newArticle,
    };

    const response = this.httpClient.post<Response<CreateArticleResponse>>("/api/articles/create", dto);
    return this.handleResponse(response);
  }

  updateArticle(id: string, article: Article): Observable<UpdateArticleResponse> {
    const dto: UpdateArticleRequest = { id, article };

    const response = this.httpClient.post<Response<UpdateArticleResponse>>("/api/articles/update", dto);
    return this.handleResponse(response);
  }

  deleteArticle(id: string): Observable<DeleteArticleResponse> {
    const response = this.httpClient.delete<Response<DeleteArticleResponse>>(`/api/articles/${id}`);
    return this.handleResponse(response);
  }

  approveArticle(id: string): Observable<ApproveArticleResponse> {
    const dto: ApproveArticleRequest = {
      articleId: id,
    };

    const response = this.httpClient.post<Response<ApproveArticleResponse>>("/api/articles/approve", dto);
    return this.handleResponse(response);
  }

  declineArticle(id: string): Observable<DeclineArticleResponse> {
    const dto: DeclineArticleRequest = {
      articleId: id,
    };

    const response = this.httpClient.post<Response<ApproveArticleResponse>>("/api/articles/decline", dto);
    return this.handleResponse(response);
  }
}
