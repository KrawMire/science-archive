import { Component } from '@angular/core';
import { BehaviorSubject } from "rxjs";
import { AuthService } from "@modules/auth/services/auth.service";
import { NzMessageService } from "ng-zorro-antd/message";
import { Title } from "@angular/platform-browser";
import { NzI18nService } from "ng-zorro-antd/i18n";

@Component({
  selector: 'sar-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss']
})
export class SignInPageComponent {
  isConfirmation$ = new BehaviorSubject<boolean>(false);

  constructor(
    private readonly i18nService: NzI18nService,
    private readonly messageService: NzMessageService,
    private readonly authService: AuthService,
    titleService: Title,
  ) {
    titleService.setTitle("Science Archive - Sign In")
  }

  async onSignedIn() {
    const user = this.authService.getCurrentAuthUser();

    if (!user) {
      this.messageService.error(this.i18nService.translate("authPage.errors.userNotPresent"));
      return;
    }

    if(!user.isConfirmed) {
      this.messageService.success(this.i18nService.translate("authPage.messages.verificationCodeSent"));
      this.isConfirmation$.next(true);
    } else {
      this.authService.deleteCurrentAuthUser();
      this.authService.saveCurrentUser(user);
      window.location.assign("/content");
    }
  }
}
