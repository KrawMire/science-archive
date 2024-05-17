import { Component, OnInit } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { AuthService } from "@modules/auth/services/auth.service";
import { User } from "@models/user/user";
import { CookieService } from "ngx-cookie-service";
import { AvailableClaims } from "@models/claims/available-claims";

@Component({
  selector: "sar-content-page",
  templateUrl: "./content-page.component.html",
  styleUrls: ["./content-page.component.scss"],
})
export class ContentPageComponent implements OnInit {
  showAccountDrawer$ = new BehaviorSubject<boolean>(false);
  currentUser$ = new BehaviorSubject<User | null>(null);
  currentClaims$ = new BehaviorSubject<string[]>([]);

  constructor(
    private readonly authService: AuthService,
    private readonly cookieService: CookieService
  ) {
    this.currentUser$.next(this.authService.getCurrentUser());
    this.currentClaims$.next(this.authService.getCurrentClaims());
  }

  ngOnInit(): void {

  }

  onSignOut() {
    this.cookieService.delete("Authorization", "/");
    this.authService.deleteCurrentUser();
    this.authService.deleteCurrentClaims();
    window.location.reload();
  }

  onAccountClick() {
    this.showAccountDrawer$.next(true);
  }

  onAccountDrawerClose() {
    this.showAccountDrawer$.next(false);
  }

  protected readonly AvailableClaims = AvailableClaims;
}
