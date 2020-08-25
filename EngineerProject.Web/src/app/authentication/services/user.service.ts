import { SharedDataService } from '../../shared/services/shared-data.service';
import { UserProfileData } from '../../profile/interfaces/UserProfileData';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {

  constructor(private http: HttpClient, private sharedData: SharedDataService) {
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
          this.sharedData.userSubject.next(newUser);
          return newUser;
      }));
  }

  logout() {
      localStorage.removeItem('currentUser');
      this.sharedData.userSubject.next(null);
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
