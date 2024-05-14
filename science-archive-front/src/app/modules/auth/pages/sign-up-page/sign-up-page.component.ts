import { Component } from '@angular/core';
import { BehaviorSubject } from "rxjs";
import { NzMessageService } from "ng-zorro-antd/message";
import { AuthService } from "@modules/auth/services/auth.service";

@Component({
  selector: 'sar-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent {
  isConfirmation$ = new BehaviorSubject<boolean>(false);

  constructor(
    private readonly messageService: NzMessageService,
    private readonly authService: AuthService,
  ) {}

  onSignedUp() {
    const user = this.authService.getCurrentAuthUser();

    if (!user) {
      this.messageService.error("User is not present");
      return;
    }

    this.messageService.success("A confirmation email has been sent to your email address");
    this.isConfirmation$.next(true);
  }
}
