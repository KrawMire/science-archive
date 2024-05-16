import { Component, EventEmitter, Output } from "@angular/core";
import { FormControl, FormGroup, NonNullableFormBuilder, Validators } from "@angular/forms";
import { AuthApiService } from "@services/auth-api.service";
import { NzMessageService } from "ng-zorro-antd/message";
import { AuthService } from "@modules/auth/services/auth.service";

@Component({
  selector: 'sar-sign-up-form',
  templateUrl: './sign-up-form.component.html',
  styleUrls: ['./sign-up-form.component.scss']
})
export class SignUpFormComponent {
  @Output() onSignedUp = new EventEmitter<void>();

  validateForm: FormGroup<{
    firstName: FormControl<string>;
    secondName: FormControl<string>;
    login: FormControl<string>;
    email: FormControl<string>;
    password: FormControl<string>;
    repeatPassword: FormControl<string>;
  }> = this.fb.group({
    firstName: ['', [Validators.minLength(2), Validators.required]],
    secondName: ['', [Validators.minLength(2), Validators.required]],
    login: ['', [Validators.minLength(3), Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.minLength(10), Validators.required]],
    repeatPassword: ['', [Validators.minLength(10), Validators.required]],
  });

  constructor(
    private fb: NonNullableFormBuilder,
    private messageService: NzMessageService,
    private readonly authService: AuthService,
    private readonly authApiService: AuthApiService) {}

  submitForm() {
    if (this.validateForm.controls.password.value !== this.validateForm.controls.repeatPassword.value) {
      this.validateForm.controls.repeatPassword.setErrors({required: true});
      return;
    }

    if (this.validateForm.valid) {
      this.signUp();
      return;
    }

    Object.values(this.validateForm.controls).forEach(control => {
      if (control.invalid) {
        control.markAsDirty();
        control.updateValueAndValidity({ onlySelf: true });
      }
    });
  }

  signUp() {
    const username = this.validateForm.controls.firstName.value + " " + this.validateForm.controls.secondName.value;
    this.authApiService.signUp(
      username,
      this.validateForm.controls.email.value,
      this.validateForm.controls.login.value,
      this.validateForm.controls.password.value,
    ).subscribe({
      next: (response) => {
        this.authService.saveCurrentAuthUser(response.user);
        this.onSignedUp.emit();
      },
      error: (error) => {
        this.messageService.error(error.message ?? "Unhandled error occurred.");
      }
    });
  }
}
