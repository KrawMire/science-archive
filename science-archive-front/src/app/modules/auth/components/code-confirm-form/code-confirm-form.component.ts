import { Component } from '@angular/core';
import { NzMessageService } from "ng-zorro-antd/message";
import { AuthService } from "@modules/auth/services/auth.service";
import { AuthApiService } from "@services/auth-api.service";
import { NonNullableFormBuilder, Validators } from "@angular/forms";
import { ConfirmUserResponse } from "@models/auth/responses/confirm-user.response";
import { Router } from "@angular/router";

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
    private router: Router,
    private readonly messageService: NzMessageService,
    private readonly authService: AuthService,
    private readonly authApiService: AuthApiService,
  ) {
  }

  resendConfirmationCode() {
    const user = this.authService.getCurrentAuthUser();

    if (!user) {
      this.messageService.error("User is not present");
      return;
    }

    if (this.resendTimes >= 5) {
      this.messageService.error("Code was resent over 5 times");
      return;
    }

    this.messageService.info("Verification code was send to your email!");
    this.resendTimes++;
  }

  confirm() {
    const user = this.authService.getCurrentAuthUser();

    if (!user?.id) {
      this.messageService.error("User is not present");
      return;
    }

    if (this.formGroup.valid) {
      const code = this.formGroup.controls.code.value;
      this.authApiService
        .confirmUserWithCode(user.id, code)
        .subscribe({
          next: async (response: ConfirmUserResponse) => {
            this.authService.saveCurrentAuthUser(response.user);
            await this.router.navigate(['/content']);
          },
          error: (error: any) => {
            this.messageService.error(error.message ?? error);
          }
        });
      return;
    } else {
      this.messageService.error("Code is invalid");
    }
  }
}
