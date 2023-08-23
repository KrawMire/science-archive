import { Component, OnInit } from "@angular/core";
import { LocalStorageService } from "src/app/services/local-storage.service";

@Component({
  selector: "app-main-page",
  templateUrl: "./main-page.component.html",
  styleUrls: ["./main-page.component.scss"],
})
export class MainPageComponent implements OnInit {
  login: string = "";
  isAuthorized: boolean = false;
  isMobileMenuOpen: boolean = false;

  constructor(private storageService: LocalStorageService) {}

  ngOnInit(): void {
    const login = this.storageService.getLogin();

    if (login) {
      this.login = login;
      this.isAuthorized = true;
    }
  }

  onSignOut() {
    this.storageService.clean();
  }

  onToggleMenu() {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }

  onCloseMenu() {
    this.isMobileMenuOpen = false;
  }

  protected readonly ontoggle = ontoggle;
}
