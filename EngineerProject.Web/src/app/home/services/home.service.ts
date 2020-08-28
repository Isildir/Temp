import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { GroupTile } from 'src/app/home/interfaces/GroupTile';
import { HomeGroupWrapper } from '../interfaces/HomeGroupWrapper';
import { HttpRequestWrapperService } from 'src/app/core/services/http-request-wrapper.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  constructor(private httpRequestWrapper: HttpRequestWrapperService) {
  }

  getUserGroups() {
    const url = `${environment.apiUrl}groups/GetUserGroups`;

    return this.httpRequestWrapper.get<HomeGroupWrapper>(url);
  }

  getGroups(filter: string) {
    const url = `${environment.apiUrl}groups/Get?pageSize=5&page=1&filter=${filter}`;

    return this.httpRequestWrapper.get<GroupTile[]>(url);
  }

  createGroup(name: string, description: string, isPrivate: boolean) {
    const url = `${environment.apiUrl}groups/Create`;

    return this.httpRequestWrapper.post<GroupTile>(url, { name, description, isPrivate });
  }

  askForInvite(id: number) {
    const url = `${environment.apiUrl}groups/AskForInvite`;

    return this.httpRequestWrapper.postBoolean(url, { id });
  }

  resolveGroupInvite(id: number, value: boolean) {
    const url = `${environment.apiUrl}groups/ResolveGroupInvite`;

    return this.httpRequestWrapper.postBoolean(url, { id, value });
  }
}