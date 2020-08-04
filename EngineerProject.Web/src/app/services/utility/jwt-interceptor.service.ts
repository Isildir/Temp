import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { UserService } from '../data-services/user.service';
import { CustomHttpParams } from './customHttpParams';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authenticationService: UserService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add authorization header with jwt token if available
        const currentUser = this.authenticationService.currentUserValue;

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
