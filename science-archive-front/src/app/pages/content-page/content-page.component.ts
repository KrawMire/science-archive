import { Component } from "@angular/core";

@Component({
  selector: "sar-content-page",
  templateUrl: "./content-page.component.html",
  styleUrls: ["./content-page.component.scss"],
})
export class ContentPageComponent {
  showAccountDrawer = false;

  onAccountClick() {
    this.showAccountDrawer = true;
  }

  onAccountDrawerClose() {
    this.showAccountDrawer = false;
  }
}
