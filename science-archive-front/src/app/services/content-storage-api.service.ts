import { ApiService } from "@services/common/api.service";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Response } from "@models/common/response";
import { UploadDocumentResponse } from "@models/content-storage/responses/upload-document.response";

@Injectable({
  providedIn: "root",
})
export class ContentStorageService extends ApiService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  uploadDocument(file: any): Observable<UploadDocumentResponse> {
    const formData = new FormData();
    formData.append("document", file);

    const response = this.httpClient.post<Response<UploadDocumentResponse>>("/api/documents/upload", formData);
    return this.handleResponse(response);
  }
}
