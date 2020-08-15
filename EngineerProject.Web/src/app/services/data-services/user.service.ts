import { UserData } from './../../models/UserData';
import { UserProfileData } from '../../models/UserProfileData';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {
  private currentUserSubject: BehaviorSubject<UserData>;
  public currentUser: Observable<UserData>;

  constructor(private http: HttpClient) {
      this.currentUserSubject = new BehaviorSubject<UserData>(JSON.parse(localStorage.getItem('currentUser')));
      this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): UserData {
      return this.currentUserSubject.value;
  }

  login(identifier: string, password: string) {
      const url = `${environment.apiUrl}users/authenticate`;

      return this.http.post<any>(url, { identifier, password })
      .pipe(map(user => {
          const newUser = {
            login: user.login,
            token: user.token
          };

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

  register(login: string, email: string, password: string) {
      const url = `${environment.apiUrl}users/register`;

      return this.http.post<any>(url, { login, email, password });
  }

  getProfile() {
    const url = `${environment.apiUrl}users/GetProfile`;

    return this.http.get<UserProfileData>(url);
  }

  changeNotificationSettings() {
    const url = `${environment.apiUrl}users/ChangeNotificationSettings`;

    return this.http.post<any>(url, {});
  }

  changePassword(oldPassword: string, newPassword: string) {
    const url = `${environment.apiUrl}users/ChangePassword`;

    return this.http.post<any>(url, { oldPassword, password: newPassword });
  }

  sendPasswordRecovery(identifier: string) {
    const url = `${environment.apiUrl}users/SendPasswordRecovery`;

    return this.http.post<any>(url, JSON.stringify(identifier));
  }

  checkPasswordRecovery(code: string) {
    const url = `${environment.apiUrl}users/CheckPasswordRecovery`;

    const options = { headers: new HttpHeaders({'Content-Type': 'application/json'}) };

    return this.http.post<any>(url,  JSON.stringify(code), options);
  }

  usePasswordRecovery(identifier: string, password: string, code: string) {
    const url = `${environment.apiUrl}users/UsePasswordRecovery`;

    return this.http.post<any>(url, { identifier, code, password});
  }
}
