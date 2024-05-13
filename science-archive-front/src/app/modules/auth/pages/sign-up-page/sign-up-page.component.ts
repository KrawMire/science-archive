import { Component } from '@angular/core';
import { BehaviorSubject } from "rxjs";

@Component({
  selector: 'sar-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent {
  isConfirmation$ = new BehaviorSubject<boolean>(false);

  onSignedUp() {
    this.isConfirmation$.next(true);
  }
}
