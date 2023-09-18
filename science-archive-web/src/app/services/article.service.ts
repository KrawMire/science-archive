import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Response } from "@models/common/response";
import { GetArticleByIdResponse } from "@models/article/responses/get-article-by-id.response";
import { GetAllArticlesResponse } from "@models/article/responses/get-all-articles.response";
import { GetArticlesByCategoryIdResponse } from "@models/article/responses/get-articles-by-category-id.response";
import { ApiService } from "@services/api.service";

@Injectable({
  providedIn: "root",
})
export class ArticleService extends ApiService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  getAllArticles(): Observable<GetAllArticlesResponse> {
    const response = this.httpClient.get<Response<GetAllArticlesResponse>>("/api/articles/");
    return this.handleResponse(response);
  }

  getArticlesByCategoryId(categoryId: string): Observable<GetArticlesByCategoryIdResponse> {
    const response = this.httpClient.get<Response<GetArticlesByCategoryIdResponse>>(
      `/api/articles/by-category/${categoryId}`
    );
    return this.handleResponse(response);
  }

  getArticleById(id: string): Observable<GetArticleByIdResponse> {
    const response = this.httpClient.get<Response<GetArticleByIdResponse>>(`/api/articles/${id}`);
    return this.handleResponse(response);
  }
}
