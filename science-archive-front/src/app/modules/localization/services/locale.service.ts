import { Injectable } from '@angular/core';

export enum Locales {
  RU = "RU",
  EN = "EN"
}

@Injectable({
  providedIn: 'root'
})
export class LocaleService {
  private locale: string = Locales.RU;

  constructor() {
    if (navigator.language.startsWith("en")) {
      this.locale = Locales.EN;
    } else {
      this.locale = Locales.RU;
    }
  }

  setLocale(locale: Locales) {
    this.locale = locale || Locales.EN;
  }

  getCurrentLocale() {
    return this.locale;
  }
}
