import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Response } from "@models/common/response";
import { GetAllUsersResponse } from "@models/user/responses/get-all-users.response";

@Injectable({
  providedIn: "root",
})
export class UserService {
  constructor(private httpClient: HttpClient) {}

  public getAllUsers(): Observable<Response<GetAllUsersResponse>> {
    return this.httpClient.get<Response<GetAllUsersResponse>>("/api/users/");
  }
}
