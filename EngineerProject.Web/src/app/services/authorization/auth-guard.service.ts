import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthenticationService } from './authentication.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const currentUser = this.authenticationService.currentUserValue;
/*
        if (currentUser) {
            if (route.routeConfig.path.indexOf('clients') !== -1 && currentUser.IsAdmin === false) {
                return false;
            } else if (route.routeConfig.path.indexOf('logs') !== -1 && currentUser.IsAdmin === false) {
                return false;
            } else if (route.routeConfig.path.indexOf('files') !== -1 && currentUser.IsAdmin === true) {
                return false;
            } else if (route.routeConfig.path.indexOf('profile') !== -1 && currentUser.IsAdmin === true) {
                return false;
            }

            return true;
        }
*/
        // not logged in so redirect to login page with the return url
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });

        return false;
    }
}
