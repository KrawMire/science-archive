import { Component } from '@angular/core';
import { CookieService } from "ngx-cookie-service";
import { AuthService } from "@modules/auth/services/auth.service";
import { User } from "@models/user/user";
import { AvailableClaims } from "@models/claims/available-claims";

@Component({
  selector: 'sar-account-page',
  templateUrl: './account-page.component.html',
  styleUrls: ['./account-page.component.scss']
})
export class AccountPageComponent {
  readonly user: User | null;
  readonly isAdmin: boolean;

  constructor(
    private readonly authService: AuthService,
    private readonly cookieService: CookieService,
  ) {
    this.user = this.authService.getCurrentUser();
    this.isAdmin = this.authService.getCurrentClaims().includes(AvailableClaims.AccessAdminPage);
  }

  signOut(): void {
    this.cookieService.delete("Authorization", "/");
    this.authService.deleteCurrentUser();
    this.authService.deleteCurrentClaims();
    window.location.assign("/");
  }
}
