import { Component } from '@angular/core';
import { NzMessageService } from "ng-zorro-antd/message";
import { AuthService } from "@modules/auth/services/auth.service";
import { AuthApiService } from "@services/auth-api.service";
import { NonNullableFormBuilder, Validators } from "@angular/forms";
import { ConfirmUserResponse } from "@models/auth/responses/confirm-user.response";
import { NzI18nService } from "ng-zorro-antd/i18n";

@Component({
  selector: 'sar-code-confirm-form',
  templateUrl: './code-confirm-form.component.html',
  styleUrls: ['./code-confirm-form.component.scss']
})
export class CodeConfirmFormComponent {
  resendTimes = 1;

  formGroup = this.fb.group({
    code: ['', [Validators.minLength(6), Validators.maxLength(6), Validators.required]],
  });

  constructor(
    private fb: NonNullableFormBuilder,
    private readonly i18nService: NzI18nService,
    private readonly messageService: NzMessageService,
    private readonly authService: AuthService,
    private readonly authApiService: AuthApiService,
  ) {
  }

  resendConfirmationCode() {
    const user = this.authService.getCurrentAuthUser();

    if (!user?.id) {
      this.messageService.error(this.i18nService.translate("authPage.errors.userNotPresent"));
      return;
    }

    if (this.resendTimes >= 5) {
      this.messageService.error(this.i18nService.translate("authPage.errors.codeWasResentManyTimes"));
      return;
    }

    this.authApiService
      .resendConfirmationCode(user.id)
      .subscribe({
        next: () => {
          this.messageService.info(this.i18nService.translate("authPage.messages.verificationCodeSent"));
          this.resendTimes++;
        },
        error: (error) => {
          this.messageService.error(error.message);
        }
      });
  }

  confirm() {
    const user = this.authService.getCurrentAuthUser();

    if (!user?.id) {
      this.messageService.error(this.i18nService.translate("authPage.errors.userNotPresent"));
      return;
    }

    if (this.formGroup.valid) {
      const code = this.formGroup.controls.code.value;
      this.authApiService
        .confirmUserWithCode(user.id, code)
        .subscribe({
          next: async (response: ConfirmUserResponse) => {
            this.authService.deleteCurrentAuthUser();
            this.authService.saveCurrentUser(response.user);
            window.location.assign("/content");
          },
          error: (error: any) => {
            console.log(error);
            this.messageService.error(error.message ?? this.i18nService.translate("commonErrors.unhandledError"));
          }
        });
      return;
    } else {
      this.messageService.error(this.i18nService.translate("authPage.errors.invalidCode"));
    }
  }
}
