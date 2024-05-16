import { Component } from "@angular/core";
import { AuthService } from "@modules/auth/services/auth.service";
import { AuthApiService } from "@services/auth-api.service";
import { NzMessageService } from "ng-zorro-antd/message";
import { CookieService } from "ngx-cookie-service";

@Component({
  selector: 'sar-app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(
    private readonly messageService: NzMessageService,
    private readonly authApiService: AuthApiService,
    private readonly authService: AuthService,
    private readonly cookieService: CookieService,
  ) {
    const user = this.authService.getCurrentUser();

    if (!user) {
      return;
    }

    this.authApiService.getMe().subscribe({
      next: response => {
        this.authService.saveCurrentUser(response.user);
        this.authService.saveCurrentClaims(response.claims);
      },
      error: err => {
        if (!err.status || err.status !== 401) {
          this.messageService.error("Unhandled error occurred!");
        }

        this.authService.deleteCurrentUser();
        this.authService.deleteCurrentClaims();
        this.cookieService.delete("Authorization");
        window.location.reload();
      }
    });
  }
}
