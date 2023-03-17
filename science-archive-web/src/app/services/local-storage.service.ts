import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  constructor() {}

  clean(): void {
    localStorage.clear();
  }

  public saveToken(token: string) {
    localStorage.removeItem("api_token");
    localStorage.setItem("api_token", token);
  }

  public getToken(): string | null {
    const token = localStorage.getItem("api_token");

    return token;
  }

  public isLoggedIn(): boolean {
    const token = this.getToken();

    return !!token;
  }
}