import { UserProfileData } from './../../models/UserProfileData';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { environment } from '../../../environments/environment';
import { User } from '../../models/User';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(email: string, password: string) {
        const url = `${environment.apiUrl}users/authenticate`;
        const options = { headers: new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'}) };

        return this.http.post<any>(url, { email, password, grant_type: 'password' }, options)
        .pipe(map(user => {
            const newUser = new User();

            newUser.Email = user.email;
            newUser.Token = user.access_token;

            localStorage.setItem('currentUser', JSON.stringify(newUser));
            this.currentUserSubject.next(newUser);
            return newUser;
        }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

    register(email: string, password: string) {
        const url = `${environment.apiUrl}users/register`;
        const options = { headers: new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'}) };

        return this.http.post<any>(url, { email, password }, options);
    }

  getProfile() {
    const url = `${environment.apiUrl}users/GetProfile`;

    return this.http.get<UserProfileData>(url);
  }

  changeEmailReceiving() {
    const url = `${environment.apiUrl}users/ChangeEmailReceiving`;

    const options = { headers: new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'}) };

    return this.http.post<any>(url, {} , options);
  }

  changePassword(oldPassword: string, newPassword: string) {
    const url = `${environment.apiUrl}users/ChangePassword`;

    const options = { headers: new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'}) };

    return this.http.post<any>(url, { oldPassword, password: newPassword }, options);
  }

  sendPasswordRecovery(userLogin: string) {
    const url = `${environment.apiUrl}users/SendPasswordRecovery`;

    const options = { headers: new HttpHeaders({'Content-Type': 'application/json'}) };

    return this.http.post<any>(url, JSON.stringify(userLogin), options);
  }

  checkPasswordRecovery(code: string) {
    const url = `${environment.apiUrl}users/CheckPasswordRecovery`;

    const options = { headers: new HttpHeaders({'Content-Type': 'application/json'}) };

    return this.http.post<any>(url,  JSON.stringify(code), options);
  }

  usePasswordRecovery(login: string, email: string, password: string, code: string) {
    const url = `${environment.apiUrl}users/UsePasswordRecovery`;

    const options = { headers: new HttpHeaders({'Content-Type': 'application/json'}) };

    return this.http.post<any>(url, { login, code, email, password}, options);
  }
}