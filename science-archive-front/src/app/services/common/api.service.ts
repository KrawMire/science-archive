import { catchError, map, Observable } from "rxjs";
import { Response } from "@models/common/response";
import { ApiError } from "@services/common/api-error";

export abstract class ApiService {
  protected handleResponse<T>(res: Observable<Response<T>>) {
    return res.pipe(
      map((response) => {
        if (!response.success) {
          throw new Error(response.error ?? "Unknown error while request execution");
        }

        if (!response.data) {
          throw new Error("Cannot get any data!");
        }

        return response.data!;
      }),
      catchError((err) => {
        throw new ApiError(err.error?.error ?? err.message, err.status);
      })
    );
  }
}
