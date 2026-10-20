import { Component, Input } from "@angular/core";
import { LocaleService, Locales } from "@modules/localization/services/locale.service";

@Component({
  selector: "sar-site-footer",
  templateUrl: "./site-footer.component.html",
  styleUrls: ["./site-footer.component.scss"],
})
export class SiteFooterComponent {
  /**
   * The full footer (mission, menu, contacts) is shown on the home page; other pages get a compact strip
   */
  @Input() full = false;

  readonly Locales = Locales;
  readonly locale: string;

  readonly telegramUrl = "https://t.me/science_archive_project";
  readonly linkedInUrl = "https://www.linkedin.com/in/anton-gavrilov-backend/";
  readonly repositoryUrl = "https://github.com/KrawMire/science-archive";
  readonly email = "science.archive.supp@gmail.com";

  constructor(private readonly localeService: LocaleService) {
    this.locale = this.localeService.getCurrentLocale();
  }

  setLocale(locale: Locales): void {
    this.localeService.switchLocale(locale);
  }
}
