import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { GroupTile } from 'src/app/home/interfaces/GroupTile';
import { HomeGroupWrapper } from '../interfaces/HomeGroupWrapper';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  constructor(private http: HttpClient) {
  }

  getUserGroups() {
    const url = `${environment.apiUrl}groups/GetUserGroups`;

    return this.http.get<HomeGroupWrapper>(url);
  }

  getGroups(filter: string) {
    const url = `${environment.apiUrl}groups/Get?pageSize=5&page=1&filter=${filter}`;

    return this.http.get<GroupTile[]>(url);
  }

  createGroup(name: string, description: string, isPrivate: boolean) {
    const url = `${environment.apiUrl}groups/Create`;

    return this.http.post<any>(url, { name, description, isPrivate });
  }

  askForInvite(id: number) {
    const url = `${environment.apiUrl}groups/AskForInvite`;

    return this.http.post<any>(url, { id });
  }

  resolveGroupInvite(id: number, value: boolean) {
    const url = `${environment.apiUrl}groups/ResolveGroupInvite`;

    return this.http.post<any>(url, { id, value });
  }
}