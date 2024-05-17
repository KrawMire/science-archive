import { CanActivateFn, Router } from "@angular/router";
import { mergeMap, of } from "rxjs";
import { inject } from "@angular/core";
import { NzMessageService } from "ng-zorro-antd/message";
import { AuthService } from "@modules/auth/services/auth.service";
import { AuthApiService } from "@services/auth-api.service";
import { LoadingService } from "@modules/shared/services/loading.service";

export const isAuthorizedGuard: CanActivateFn = (
  route,
  state,
  loadingService = inject(LoadingService),
  router = inject(Router),
  messageService = inject(NzMessageService),
  authService = inject(AuthService),
  authApiService = inject(AuthApiService)) => {
  loadingService.isLoading$.next(true);
  const user = authService.getCurrentUser();

  if (!user) {
    loadingService.isLoading$.next(false);
    router.navigate(["/auth"]);
    return of(false);
  }

  return authApiService
    .getMe()
    .pipe(
      mergeMap(async response => {
        loadingService.isLoading$.next(false);

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
