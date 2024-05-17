import { Component } from '@angular/core';
import { AsyncPipe, NgIf } from "@angular/common";
import { NzButtonComponent } from "ng-zorro-antd/button";
import { NzContentComponent, NzHeaderComponent, NzLayoutComponent, NzSiderComponent } from "ng-zorro-antd/layout";
import { NzDrawerComponent, NzDrawerContentDirective } from "ng-zorro-antd/drawer";
import { NzIconDirective } from "ng-zorro-antd/icon";
import { NzMenuDirective, NzMenuItemComponent, NzSubMenuComponent } from "ng-zorro-antd/menu";
import { Router, RouterLink, RouterOutlet } from "@angular/router";
import { BehaviorSubject, map, Observable } from "rxjs";
import { BreakpointObserver, Breakpoints } from "@angular/cdk/layout";
import { NzI18nPipe } from "ng-zorro-antd/i18n";

@Component({
  selector: 'sar-admin-page',
  standalone: true,
  imports: [
    AsyncPipe,
    NgIf,
    NzButtonComponent,
    NzContentComponent,
    NzDrawerComponent,
    NzHeaderComponent,
    NzIconDirective,
    NzLayoutComponent,
    NzMenuDirective,
    NzMenuItemComponent,
    NzSiderComponent,
    NzSubMenuComponent,
    RouterLink,
    RouterOutlet,
    NzDrawerContentDirective,
    NzI18nPipe
  ],
  templateUrl: './admin-page.component.html',
  styleUrl: './admin-page.component.scss'
})
export class AdminPageComponent {
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
