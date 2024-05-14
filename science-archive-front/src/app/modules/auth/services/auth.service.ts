import { Injectable } from '@angular/core';
import { User } from "@models/user/user";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authUser: User | null = null;
  private readonly userKey: string = "userData";

  public getCurrentAuthUser(): User | null {
    return this.authUser;
  }

  public saveCurrentAuthUser(user: User): void {
    this.authUser = user;
  }

  public deleteCurrentAuthUser(): void {
    this.authUser = null;
  }

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

  public deleteCurrentUser(): void {
    localStorage.removeItem(this.userKey);
  }
}
