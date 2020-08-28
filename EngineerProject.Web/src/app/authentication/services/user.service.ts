import { HttpRequestWrapperService } from 'src/app/core/services/http-request-wrapper.service';
import { SharedDataService } from '../../shared/services/shared-data.service';
import { UserProfileData } from '../../profile/interfaces/UserProfileData';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private httpRequestWrapper: HttpRequestWrapperService, private sharedData: SharedDataService) {
  }

  login(identifier: string, password: string) {
      const url = `${environment.apiUrl}users/authenticate`;

      return this.httpRequestWrapper.post<any>(url, { identifier, password })
      .pipe(map(user => {
          const newUser = {
            login: user.data.login,
            token: user.data.token
          };
console.log(newUser);
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

      return this.httpRequestWrapper.postBoolean(url, { login, email, password });
  }

  getProfile() {
    const url = `${environment.apiUrl}users/GetProfile`;

    return this.httpRequestWrapper.get<UserProfileData>(url);
  }

  changeNotificationSettings() {
    const url = `${environment.apiUrl}users/ChangeNotificationSettings`;

    return this.httpRequestWrapper.postBoolean(url, {});
  }

  changePassword(oldPassword: string, newPassword: string) {
    const url = `${environment.apiUrl}users/ChangePassword`;

    return this.httpRequestWrapper.postBoolean(url, { oldPassword, password: newPassword });
  }

  sendPasswordRecovery(identifier: string) {
    const url = `${environment.apiUrl}users/SendPasswordRecovery`;

    return this.httpRequestWrapper.postBoolean(url, JSON.stringify(identifier));
  }

  checkPasswordRecovery(code: string) {
    const url = `${environment.apiUrl}users/CheckPasswordRecovery`;

    return this.httpRequestWrapper.postBoolean(url,  JSON.stringify(code));
  }

  usePasswordRecovery(identifier: string, password: string, code: string) {
    const url = `${environment.apiUrl}users/UsePasswordRecovery`;

    return this.httpRequestWrapper.postBoolean(url, { identifier, code, password});
  }
}