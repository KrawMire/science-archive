import { Injectable } from '@angular/core';
import { User } from "@models/user/user";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authUser: User | null = null;
  private readonly userDataKey: string = "userData";

  public getCurrentAuthUser(): User | null {
    return this.authUser;
  }

  public saveCurrentAuthUser(user: User): void {
    this.authUser = user;
  }

  public deleteCurrentAuthUser(): void {
    this.authUser = null;
  }
}
