import { Pipe, PipeTransform } from '@angular/core';
import { initials } from "@modules/shared/utils/article-utils";

@Pipe({
  name: 'initials'
})
export class InitialsPipe implements PipeTransform {
  transform(value?: string | null): string {
    return initials(value);
  }
}
