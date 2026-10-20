import { Pipe, PipeTransform } from '@angular/core';
import { LocaleService, Locales } from "@modules/localization/services/locale.service";
import { toDate } from "@modules/shared/utils/article-utils";

/**
 * Formats dates the way the design shows them: "14 Oct 2026", or "14 Oct" for the short style
 */
@Pipe({
  name: 'dateFormat'
})
export class DateFormatPipe implements PipeTransform {
  // Intl's en-GB short months include "Sept", while the design uses three-letter months throughout
  private static readonly EN_MONTHS = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

  private readonly monthFormatter: Intl.DateTimeFormat | null;

  constructor(localeService: LocaleService) {
    this.monthFormatter = localeService.getCurrentLocale() === Locales.RU
      ? new Intl.DateTimeFormat("ru-RU", { month: "short" })
      : null;
  }

  transform(value?: Date | string | null, style: "long" | "short" = "long"): string {
    const date = toDate(value);

    if (!date) {
      return "";
    }

    const day = ('0' + date.getDate()).slice(-2);
    const month = this.monthFormatter
      ? this.monthFormatter.format(date).replace(".", "")
      : DateFormatPipe.EN_MONTHS[date.getMonth()];
    const formatted = `${day} ${month.charAt(0).toUpperCase()}${month.slice(1)}`;

    return style === "short" ? formatted : `${formatted} ${date.getFullYear()}`;
  }
}
