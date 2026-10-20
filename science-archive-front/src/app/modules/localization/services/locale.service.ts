import { Injectable } from '@angular/core';

export enum Locales {
  RU = "RU",
  EN = "EN"
}

@Injectable({
  providedIn: 'root'
})
export class LocaleService {
  private readonly storageKey = "locale";
  private locale: string = Locales.RU;

  constructor() {
    const savedLocale = this.readSavedLocale();

    if (savedLocale) {
      this.locale = savedLocale;
    } else if (navigator.language.startsWith("en")) {
      this.locale = Locales.EN;
    } else {
      this.locale = Locales.RU;
    }
  }

  setLocale(locale: Locales) {
    this.locale = locale || Locales.EN;

    try {
      localStorage.setItem(this.storageKey, this.locale);
    } catch {
      // Storage can be unavailable (private mode); the choice then lasts for this page only
    }
  }

  /**
   * Translations are provided once at bootstrap, so switching the language reloads the app
   */
  switchLocale(locale: Locales) {
    if (locale === this.locale) {
      return;
    }

    this.setLocale(locale);
    window.location.reload();
  }

  getCurrentLocale() {
    return this.locale;
  }

  private readSavedLocale(): Locales | null {
    try {
      const saved = localStorage.getItem(this.storageKey);
      return saved === Locales.EN || saved === Locales.RU ? saved : null;
    } catch {
      return null;
    }
  }
}
