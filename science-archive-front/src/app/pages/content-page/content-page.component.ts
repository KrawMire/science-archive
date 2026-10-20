import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "sar-content-page",
  templateUrl: "./content-page.component.html",
  styleUrls: ["./content-page.component.scss"],
})
export class ContentPageComponent {
  /**
   * The home page is the shell mounted at the root path; it gets the full footer
   */
  readonly isHome: boolean;

  constructor(route: ActivatedRoute) {
    this.isHome = route.snapshot.routeConfig?.path === "";
  }
}
