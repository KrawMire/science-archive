import { ChangeDetectionStrategy, Component, Input } from "@angular/core";
import { ArticleStatus } from "@modules/shared/utils/article-utils";

@Component({
  selector: "sar-status-tag",
  template: `
    <span class="sa-tag" [ngClass]="'sa-tag--' + config.modifier">
      <sar-icon [name]="config.icon" [size]="12" [strokeWidth]="2.5" />
      {{ config.label | nzI18n }}
    </span>
  `,
  styles: [":host { display: inline-flex; }"],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StatusTagComponent {
  @Input({ required: true }) status!: number;

  get config(): { modifier: string; icon: string; label: string } {
    switch (this.status) {
      case ArticleStatus.Verified:
        return { modifier: "verified", icon: "check", label: "status.verified" };
      case ArticleStatus.Declined:
        return { modifier: "declined", icon: "x", label: "status.declined" };
      case ArticleStatus.ToVerify:
      default:
        return { modifier: "to-verify", icon: "clock", label: "status.toVerify" };
    }
  }
}
