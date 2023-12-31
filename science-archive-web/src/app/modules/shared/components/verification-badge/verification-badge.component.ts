import { Component, Input } from "@angular/core";

@Component({
  selector: "app-article-verification-badge",
  templateUrl: "./verification-badge.component.html",
  styleUrls: ["./verification-badge.component.scss"],
})
export class VerificationBadgeComponent {
  @Input() status = 0;
}
