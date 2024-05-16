import { CanActivateFn, Router } from "@angular/router";
import { inject } from "@angular/core";
import { AuthService } from "@modules/auth/services/auth.service";
import { AuthApiService } from "@services/auth-api.service";
import { mergeMap, of } from "rxjs";
import { NzMessageService } from "ng-zorro-antd/message";
import { AvailableClaims } from "@models/claims/available-claims";

export const isAdminGuard: CanActivateFn = (
  route,
  state,
  router = inject(Router),
  messageService = inject(NzMessageService),
  authService = inject(AuthService),
  authApiService = inject(AuthApiService)) => {
  const user = authService.getCurrentUser();

  if (!user) {
    router.navigate(["/content"]);
    return of(false);
  }

  return authApiService
    .getMe()
    .pipe(
      mergeMap(async response => {
        if (response.claims.some(c => c === AvailableClaims.AccessAdminPage)) {
          return true;
        } else {
          messageService.error("Only for administrators");
          await router.navigate(["/content"]);
          return false;
        }
      })
    );
};
