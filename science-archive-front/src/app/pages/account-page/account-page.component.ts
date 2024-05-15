import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component } from '@angular/core';
import { BehaviorSubject, map, Observable } from "rxjs";
import { Router } from "@angular/router";

@Component({
  selector: 'sar-account-page',
  templateUrl: './account-page.component.html',
  styleUrls: ['./account-page.component.scss']
})
export class AccountPageComponent {
  showMobileDrawer$ = new BehaviorSubject(false);
  isMobile$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );

  constructor(
    private readonly breakpointObserver: BreakpointObserver,
    private readonly router: Router
  ) {}

  openDrawer(): void {
    this.showMobileDrawer$.next(true);
  }

  closeDrawer(): void {
    this.showMobileDrawer$.next(false);
  }

  async onDrawerNavigateTo(...route: string[]) {
    this.closeDrawer();
    await this.router.navigate([...route]);
  }
}
