import { Component } from '@angular/core';
import { BehaviorSubject } from "rxjs";

@Component({
  selector: 'sar-account-page',
  templateUrl: './account-page.component.html',
  styleUrls: ['./account-page.component.scss']
})
export class AccountPageComponent {
  isCollapsed$ = new BehaviorSubject<boolean>(false);

  toggleCollapsed() {
    this.isCollapsed$.next(!this.isCollapsed$.value);
  }
}
