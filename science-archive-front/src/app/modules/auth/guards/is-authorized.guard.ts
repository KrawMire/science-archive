import { CanActivateFn, Router } from "@angular/router";
import { mergeMap, of } from "rxjs";
import { inject } from "@angular/core";
import { NzMessageService } from "ng-zorro-antd/message";
import { AuthService } from "@modules/auth/services/auth.service";
import { AuthApiService } from "@services/auth-api.service";

export const isAuthorizedGuard: CanActivateFn = (
  route,
  state,
  router = inject(Router),
  messageService = inject(NzMessageService),
  authService = inject(AuthService),
  authApiService = inject(AuthApiService)) => {
  const user = authService.getCurrentUser();

  if (!user) {
    router.navigate(["/auth"]);
    return of(false);
  }

  return authApiService
    .getMe()
    .pipe(
      mergeMap(async response => {
        if (!!response.user) {
          return true;
        } else {
          messageService.error("Only for authorized users");
          await router.navigate(["/auth"]);
          return false;
        }
      })
    );
};
