import { Component } from "@angular/core";
import { AuthService } from "@modules/auth/services/auth.service";
import { AuthApiService } from "@services/auth-api.service";
import { NzMessageService } from "ng-zorro-antd/message";

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
  ) {
    this.authApiService.getMe().subscribe({
      next: response => {
        this.authService.saveCurrentUser(response.user);
      },
      error: err => {
        if (!err.status || err.status !== 401) {
          this.messageService.error("Unhandled error occurred!");
        }

        const user = this.authService.getCurrentUser();

        if (!user) {
          return;
        }

        this.authService.deleteCurrentUser();
        window.location.reload();
      }
    });
  }
}
