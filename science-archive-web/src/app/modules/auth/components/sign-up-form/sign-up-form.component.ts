import { Component, EventEmitter, Output } from "@angular/core";

import { SignUpRequest } from "@models/auth/requests/sign-up.request";
import { AuthService } from "@services/auth.service";

@Component({
  selector: "app-sign-up-form",
  templateUrl: "./sign-up-form.component.html",
  styleUrls: ["./sign-up-form.component.scss"],
})
export class SignUpFormComponent {
  name: string = "";
  email: string = "";
  login: string = "";
  password: string = "";
  repeatPassword: string = "";

  @Output() onChangeForm = new EventEmitter<string>();

  constructor(private authService: AuthService) {}

  changeAuthType() {
    this.onChangeForm.next("sign-in");
  }

  async onSignUp() {
    if (this.password !== this.repeatPassword) {
      alert("Password and Repeat Password are not equal");
      return;
    }

    const request: SignUpRequest = {
      user: {
        name: this.name,
        email: this.email,
        login: this.login,
        rolesIds: [],
      },
      password: this.password,
    };

    this.authService.signUp(request).subscribe({
      next: (response) => {
        alert("You have successfully signed up!");
        this.changeAuthType();
      },
    });
  }
}
