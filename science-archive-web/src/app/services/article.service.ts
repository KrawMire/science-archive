import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Response } from "@models/common/response";
import { GetArticleByIdResponse } from "@models/article/responses/get-article-by-id.response";
import { GetAllArticlesResponse } from "@models/article/responses/get-all-articles.response";

@Injectable({
  providedIn: "root",
})
export class ArticleService {
  constructor(private httpClient: HttpClient) {}

  getAllArticles(): Observable<Response<GetAllArticlesResponse>> {
    return this.httpClient.get<Response<GetAllArticlesResponse>>("/api/articles/");
  }

  getArticleById(id: string): Observable<Response<GetArticleByIdResponse>> {
    return this.httpClient.get<Response<GetArticleByIdResponse>>(`/api/articles/${id}`);
  }
}
