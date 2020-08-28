import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericRequestResult } from 'src/app/shared/interfaces/GenericRequestResult';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceHandlerService {
  constructor(private httpClient: HttpClient) { }

  public get<ReturnDataType>(url: string): Observable<GenericRequestResult<ReturnDataType>> {
    return new Observable<GenericRequestResult<ReturnDataType>>((observer) => {
      this.httpClient.get<ReturnDataType>(url).subscribe(data => {
        observer.next({ data, isSuccessful: true });
      }, error => {
        observer.next({ errorMessage: error, isSuccessful: true });
      });
    });
  }
}
