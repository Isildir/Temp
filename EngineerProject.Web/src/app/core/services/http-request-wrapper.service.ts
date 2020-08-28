import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericRequestResult } from 'src/app/shared/interfaces/GenericRequestResult';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpRequestWrapperService {
  constructor(private httpClient: HttpClient) { }

  public getBoolean(url: string): Observable<boolean> {
    return this.formatRequestBooleanResponse(this.httpClient.get(url));
  }

  public postBoolean(url: string, body: any): Observable<boolean> {
    return this.formatRequestBooleanResponse(this.httpClient.post(url, body));
  }

  public putBoolean(url: string, body: any): Observable<boolean> {
    return this.formatRequestBooleanResponse(this.httpClient.put(url, body));
  }

  public deleteBoolean(url: string): Observable<boolean> {
    return this.formatRequestBooleanResponse(this.httpClient.delete(url));
  }

  public get<ReturnDataType>(url: string, options?: {}): Observable<GenericRequestResult<ReturnDataType>> {
    return this.formatRequestResponse(this.httpClient.get<ReturnDataType>(url, options));
  }

  public post<ReturnDataType>(url: string, body: any, options?: {}): Observable<GenericRequestResult<ReturnDataType>> {
    return this.formatRequestResponse(this.httpClient.post<ReturnDataType>(url, body, options));
  }

  public put<ReturnDataType>(url: string, body: any, options?: {}): Observable<GenericRequestResult<ReturnDataType>> {
    return this.formatRequestResponse(this.httpClient.put<ReturnDataType>(url, body, options));
  }

  public delete<ReturnDataType>(url: string, options?: {}): Observable<GenericRequestResult<ReturnDataType>> {
    return this.formatRequestResponse(this.httpClient.delete<ReturnDataType>(url, options));
  }

  private formatRequestBooleanResponse(request: Observable<any>) {
    return new Observable<boolean>((observer) => {
      request.subscribe(
        () => observer.next(true),
        () => observer.next(false)
      );
    });
  }

  private formatRequestResponse<ReturnDataType>(request: Observable<ReturnDataType>) {
    return new Observable<GenericRequestResult<ReturnDataType>>((observer) => {
      request.subscribe(
        data => observer.next({ data, isSuccessful: true }),
        error => observer.next({ errorMessage: error ?? 'Nieoczekiwany błąd', isSuccessful: true })
      );
    });
  }
}
