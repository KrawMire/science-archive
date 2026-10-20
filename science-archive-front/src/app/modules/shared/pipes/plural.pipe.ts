import { Pipe, PipeTransform } from '@angular/core';
import { NzI18nService } from "ng-zorro-antd/i18n";
import { LocaleService, Locales } from "@modules/localization/services/locale.service";

/**
 * Picks a plural form from a translation group, e.g. `2 | plural:'documents.count'` reads
 * `documents.count.one|few|many|other` and replaces %count% with the number
 */
@Pipe({
  name: 'plural'
})
export class PluralPipe implements PipeTransform {
  private readonly rules: Intl.PluralRules;

  constructor(
    private readonly i18nService: NzI18nService,
    localeService: LocaleService,
  ) {
    this.rules = new Intl.PluralRules(localeService.getCurrentLocale() === Locales.RU ? "ru-RU" : "en-GB");
  }

  transform(count: number | null | undefined, key: string): string {
    const value = count ?? 0;
    const formKey = `${key}.${this.rules.select(value)}`;
    let template = this.i18nService.translate(formKey);

    if (!template || template === formKey) {
      template = this.i18nService.translate(`${key}.other`);
    }

    return template.replace("%count%", value.toString());
  }
}
