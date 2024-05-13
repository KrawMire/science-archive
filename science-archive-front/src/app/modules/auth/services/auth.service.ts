import { Injectable } from '@angular/core';
import { User } from "@models/user/user";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly userKey: string = "user";

  public getCurrentUser(): User | null {
    const json = localStorage.getItem(this.userKey);

    if (!json) {
      return null;
    }

    return JSON.parse(json);
  }

  public saveCurrentUser(user: User): void {
    const json = JSON.stringify(user);
    localStorage.setItem(this.userKey, json);
  }
}
