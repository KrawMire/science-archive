import { Component, EventEmitter, Output } from "@angular/core";
import { FormControl, FormGroup, NonNullableFormBuilder, Validators } from "@angular/forms";
import { AuthService } from "@modules/auth/services/auth.service";
import { AuthApiService } from "@services/auth-api.service";
import { NzMessageService } from "ng-zorro-antd/message";

@Component({
  selector: 'sar-sign-in-form',
  templateUrl: './sign-in-form.component.html',
  styleUrls: ['./sign-in-form.component.scss']
})
export class SignInFormComponent {
  @Output() onSignedIn = new EventEmitter<void>();

  validateForm: FormGroup<{
    login: FormControl<string>;
    password: FormControl<string>;
  }> = this.fb.group({
    login: ['', [Validators.minLength(3), Validators.required]],
    password: ['', [Validators.minLength(5), Validators.required]],
  });

  constructor(
    private readonly fb: NonNullableFormBuilder,
    private readonly messageService: NzMessageService,
    private readonly authService: AuthService,
    private readonly authApiService: AuthApiService,
  ) {}

  submitForm() {
    if (this.validateForm.valid) {
      this.signIn();
      return;
    }

    Object.values(this.validateForm.controls).forEach(control => {
      if (control.invalid) {
        control.markAsDirty();
        control.updateValueAndValidity({ onlySelf: true });
      }
    });
  }

  signIn() {
    this.authApiService.signIn(
      this.validateForm.controls.login.value,
      this.validateForm.controls.password.value
    ).subscribe({
      next: (response) => {
        this.authService.saveCurrentAuthUser(response.user);
        this.onSignedIn.emit();
      },
      error: (error) => (this.messageService.error(error.message ?? error))
    });
  }
}
