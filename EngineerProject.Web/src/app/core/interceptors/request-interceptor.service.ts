import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SharedDataService } from 'src/app/shared/services/shared-data.service';
import { CustomHttpParams } from '../utility/customHttpParams';

@Injectable()
export class RequestInterceptor implements HttpInterceptor {
    constructor(private sharedData: SharedDataService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const currentUser = this.sharedData.userSubject.value;

        console.log(request);
        if (!(request.params instanceof CustomHttpParams && (request.params as CustomHttpParams).passHeaderCheck)) {
            if (!request.headers.has('Content-Type')) {
                request = request.clone({
                    setHeaders: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                });
            }
        }

        if (currentUser && currentUser.token) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.token}`
                }
            });
        }

        return next.handle(request);
    }
}
