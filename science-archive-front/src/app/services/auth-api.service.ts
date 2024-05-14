import { Injectable } from "@angular/core";
import { ApiService } from "@services/common/api.service";
import { HttpClient } from "@angular/common/http";
import { SignUpRequest } from "@models/auth/requests/sign-up.request";
import { Response } from "@models/common/response";
import { SignUpResponse } from "@models/auth/responses/sign-up.response";
import { Observable } from "rxjs";
import { ConfirmUserResponse } from "@models/auth/responses/confirm-user.response";
import { ConfirmUserRequest } from "@models/auth/requests/confirm-user.request";
import { SignInResponse } from "@models/auth/responses/sign-in.response";
import { SignInRequest } from "@models/auth/requests/sign-in.request";

@Injectable({
  providedIn: "root"
})
export class AuthApiService extends ApiService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  signIn(login: string, password: string): Observable<SignInResponse> {
    const request: SignInRequest = { login, password };
    const response = this.httpClient.post<Response<SignInResponse>>('/api/auth/sign-in', request);
    return this.handleResponse(response);
  }

  signUp(name: string, email: string, login: string, password: string): Observable<SignUpResponse> {
    const request: SignUpRequest = {
      user: {
        name: name,
        email: email,
        login: login,
        isConfirmed: false,
        articles: []
      },
      password: password,
    };
    const response = this.httpClient.post<Response<SignUpResponse>>('/api/auth/sign-up', request);
    return this.handleResponse(response);
  }

  confirmUserWithCode(userId: string, code: string): Observable<ConfirmUserResponse> {
    const request: ConfirmUserRequest = {
      userId: userId,
      confirmCode: code
    };

    const response = this.httpClient.post<Response<ConfirmUserResponse>>('/api/auth/confirm', request);
    return this.handleResponse(response);
  }
}
