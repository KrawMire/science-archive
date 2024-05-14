import { Component } from '@angular/core';
import { BehaviorSubject } from "rxjs";
import { AuthService } from "@modules/auth/services/auth.service";
import { NzMessageService } from "ng-zorro-antd/message";
import { Router } from "@angular/router";

@Component({
  selector: 'sar-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss']
})
export class SignInPageComponent {
  isConfirmation$ = new BehaviorSubject<boolean>(false);

  constructor(
    private readonly router: Router,
    private readonly messageService: NzMessageService,
    private readonly authService: AuthService,
  ) {}

  async onSignedIn() {
    const user = this.authService.getCurrentAuthUser();

    if (!user) {
      this.messageService.error("User is not present");
      return;
    }

    if(!user.isConfirmed) {
      this.messageService.success("A confirmation email has been sent to your email address");
      this.isConfirmation$.next(true);
    } else {
      await this.router.navigate(['/content']);
    }
  }
}
