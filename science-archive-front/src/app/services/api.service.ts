import { catchError, map, Observable } from "rxjs";
import { Response } from "@models/common/response";

export abstract class ApiService {
  protected handleResponse<T>(res: Observable<Response<T>>) {
    return res.pipe(
      map((response) => {
        if (!response.success) {
          throw new Error(response.error ?? "Unknown error while request execution");
        }

        if (!response.data) {
          alert("Cannot get any data!");
          throw new Error("Cannot get any data!");
        }

        return response.data!;
      }),
      catchError((err) => {
        throw new Error(err.error?.error ?? err.message);
      })
    );
  }
}
