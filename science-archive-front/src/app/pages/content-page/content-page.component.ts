import { Component, OnInit } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { AuthService } from "@modules/auth/services/auth.service";
import { User } from "@models/user/user";

@Component({
  selector: "sar-content-page",
  templateUrl: "./content-page.component.html",
  styleUrls: ["./content-page.component.scss"],
})
export class ContentPageComponent implements OnInit {
  showAccountDrawer$ = new BehaviorSubject<boolean>(false);
  currentUser$ = new BehaviorSubject<User | null>(null);

  constructor(
    private readonly authService: AuthService,
  ) {
    this.currentUser$.next(this.authService.getCurrentAuthUser());
  }

  ngOnInit(): void {

  }

  onSignOut() {
    this.authService.deleteCurrentAuthUser();
    window.location.reload();
  }

  onAccountClick() {
    this.showAccountDrawer$.next(true);
  }

  onAccountDrawerClose() {
    this.showAccountDrawer$.next(false);
  }
}
