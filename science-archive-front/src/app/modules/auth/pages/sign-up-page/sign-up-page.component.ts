import { Component } from '@angular/core';
import { BehaviorSubject } from "rxjs";
import { NzMessageService } from "ng-zorro-antd/message";
import { AuthService } from "@modules/auth/services/auth.service";
import { Title } from "@angular/platform-browser";
import { NzI18nService } from "ng-zorro-antd/i18n";

@Component({
  selector: 'sar-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent {
  isConfirmation$ = new BehaviorSubject<boolean>(false);

  constructor(
    private readonly i18nService: NzI18nService,
    private readonly messageService: NzMessageService,
    private readonly authService: AuthService,
    titleService: Title
  ) {
    titleService.setTitle("Science Archive - Sign Up")
  }

  onSignedUp() {
    const user = this.authService.getCurrentAuthUser();

    if (!user) {
      this.messageService.error(this.i18nService.translate("authPage.errors.userNotPresent"));
      return;
    }

    this.messageService.success(this.i18nService.translate("authPage.messages.verificationCodeSent"));
    this.isConfirmation$.next(true);
  }
}
