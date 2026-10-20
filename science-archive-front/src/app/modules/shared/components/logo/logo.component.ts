import { ChangeDetectionStrategy, Component, Input } from "@angular/core";

@Component({
  selector: "sar-logo",
  template: `
    <span class="sar-logo__mark" [class.sar-logo__mark--tile]="tile">
      <img src="assets/images/science-archive.logo.svg" alt="" />
    </span>
    <span class="sar-logo__text" *ngIf="withText">Science Archive</span>
  `,
  styles: [`
    :host {
      display: inline-flex;
      align-items: center;
      gap: 8px;
      color: var(--sa-blue);
      font-weight: 800;
      font-size: 20px;
      letter-spacing: -0.02em;
      white-space: nowrap;
    }
    :host(.sar-logo--light) { color: #fff; }
    :host(.sar-logo--sm) { font-size: 15px; }
    .sar-logo__mark { display: inline-flex; align-items: center; justify-content: center; }
    .sar-logo__mark img { height: 1.4em; width: auto; display: block; }
    .sar-logo__mark--tile { width: 2em; height: 2em; background: #fff; border-radius: 10px; }
    .sar-logo__mark--tile img { height: 1.2em; }
  `],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LogoComponent {
  @Input() withText = true;
  @Input() tile = false;
}
