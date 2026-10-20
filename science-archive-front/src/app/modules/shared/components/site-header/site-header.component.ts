import { Component, ElementRef, HostListener, OnDestroy } from "@angular/core";
import { NavigationEnd, Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";
import { filter, Subscription } from "rxjs";
import { AuthService } from "@modules/auth/services/auth.service";
import { User } from "@models/user/user";
import { AvailableClaims } from "@models/claims/available-claims";
import { LocaleService, Locales } from "@modules/localization/services/locale.service";

@Component({
  selector: "sar-site-header",
  templateUrl: "./site-header.component.html",
  styleUrls: ["./site-header.component.scss"],
})
export class SiteHeaderComponent implements OnDestroy {
  readonly user: User | null;
  readonly isAdmin: boolean;
  readonly locale: string;
  readonly Locales = Locales;

  isMenuOpen = false;
  isAccountMenuOpen = false;

  private readonly navigationSubscription: Subscription;

  constructor(
    private readonly elementRef: ElementRef<HTMLElement>,
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly cookieService: CookieService,
    private readonly localeService: LocaleService,
  ) {
    this.user = this.authService.getCurrentUser();
    this.isAdmin = this.authService.getCurrentClaims().includes(AvailableClaims.AccessAdminPage);
    this.locale = this.localeService.getCurrentLocale();

    this.navigationSubscription = this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => this.closeMenus());
  }

  ngOnDestroy(): void {
    this.navigationSubscription.unsubscribe();
  }

  @HostListener("document:click", ["$event"])
  onDocumentClick(event: MouseEvent): void {
    if (!this.elementRef.nativeElement.contains(event.target as Node)) {
      this.closeMenus();
    }
  }

  @HostListener("document:keydown.escape")
  closeMenus(): void {
    this.isMenuOpen = false;
    this.isAccountMenuOpen = false;
  }

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
    this.isAccountMenuOpen = false;
  }

  toggleAccountMenu(): void {
    this.isAccountMenuOpen = !this.isAccountMenuOpen;
  }

  toggleLocale(): void {
    this.localeService.switchLocale(this.locale === Locales.EN ? Locales.RU : Locales.EN);
  }

  signOut(): void {
    this.cookieService.delete("Authorization", "/");
    this.authService.deleteCurrentUser();
    this.authService.deleteCurrentClaims();
    window.location.assign("/");
  }
}
