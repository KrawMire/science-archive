import { ChangeDetectionStrategy, Component, Input, OnChanges } from "@angular/core";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";

// Stroke icons (Lucide, ISC license) drawn on a 24×24 grid.
const ICONS: Record<string, string> = {
  "search": '<circle cx="11" cy="11" r="7"/><path d="m20 20-3.5-3.5"/>',
  "menu": '<path d="M4 6h16M4 12h16M4 18h16"/>',
  "x": '<path d="M18 6 6 18M6 6l12 12"/>',
  "plus": '<path d="M12 5v14M5 12h14"/>',
  "check": '<path d="M20 6 9 17l-5-5"/>',
  "clock": '<circle cx="12" cy="12" r="9"/><path d="M12 7v5l3 2"/>',
  "chevron-left": '<path d="m15 18-6-6 6-6"/>',
  "chevron-right": '<path d="m9 18 6-6-6-6"/>',
  "chevron-down": '<path d="m6 9 6 6 6-6"/>',
  "arrow-left": '<path d="M19 12H5M12 19l-7-7 7-7"/>',
  "arrow-right": '<path d="M5 12h14M12 5l7 7-7 7"/>',
  "download": '<path d="M12 4v11M7 10l5 5 5-5M5 20h14"/>',
  "file": '<path d="M14 3H6a1 1 0 0 0-1 1v16a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V8z"/><path d="M14 3v5h5"/>',
  "link": '<path d="M10 13a5 5 0 0 0 7.54.54l3-3a5 5 0 0 0-7.07-7.07l-1.72 1.71"/><path d="M14 11a5 5 0 0 0-7.54-.54l-3 3a5 5 0 0 0 7.07 7.07l1.71-1.71"/>',
  "quote": '<path d="M7 17c-1.7 0-3-1.3-3-3v-1c0-3 1.5-5 4-6M17 17c-1.7 0-3-1.3-3-3v-1c0-3 1.5-5 4-6"/><circle cx="7" cy="14" r="3"/><circle cx="17" cy="14" r="3"/>',
  "send": '<path d="M21 3 10 14"/><path d="m21 3-7 18-4-7-7-4z"/>',
  "share": '<path d="M12 3v12M7 8l5-5 5 5"/><path d="M5 13v6a2 2 0 0 0 2 2h10a2 2 0 0 0 2-2v-6"/>',
  "user": '<circle cx="12" cy="8" r="4"/><path d="M4 21a8 8 0 0 1 16 0"/>',
  "book": '<path d="M5 4h11a2 2 0 0 1 2 2v14H7a2 2 0 0 1-2-2z"/><path d="M5 18a2 2 0 0 1 2-2h11M9 8h5"/>',
  "shield": '<path d="M12 3 5 6v6c0 4.5 3 7.5 7 9 4-1.5 7-4.5 7-9V6z"/>',
  "log-out": '<path d="M15 4h3a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2h-3M10 17l-5-5 5-5M5 12h11"/>',
  "globe": '<circle cx="12" cy="12" r="9"/><path d="M3 12h18M12 3a14 14 0 0 1 0 18M12 3a14 14 0 0 0 0 18"/>',
  "info": '<circle cx="12" cy="12" r="9"/><path d="M12 16v-5M12 8h.01"/>',
  "external": '<path d="M14 4h6v6M20 4l-9 9M18 14v5a1 1 0 0 1-1 1H5a1 1 0 0 1-1-1V7a1 1 0 0 1 1-1h5"/>',
  "news": '<path d="M4 5h13v14H6a2 2 0 0 1-2-2z"/><path d="M17 9h3v8a2 2 0 0 1-2 2M8 9h5M8 13h5"/>',
};

@Component({
  selector: "sar-icon",
  template: `<svg
    [attr.width]="size"
    [attr.height]="size"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    [attr.stroke-width]="strokeWidth"
    stroke-linecap="round"
    stroke-linejoin="round"
    aria-hidden="true"
    [innerHTML]="path"
  ></svg>`,
  styles: [":host { display: inline-flex; line-height: 0; } svg { display: block; }"],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class IconComponent implements OnChanges {
  @Input({ required: true }) name!: string;
  @Input() size: number = 18;
  @Input() strokeWidth: number = 2;

  path: SafeHtml = "";

  constructor(private readonly sanitizer: DomSanitizer) {}

  ngOnChanges(): void {
    // The markup comes only from the static ICONS map above, never from user input.
    this.path = this.sanitizer.bypassSecurityTrustHtml(ICONS[this.name] ?? "");
  }
}
