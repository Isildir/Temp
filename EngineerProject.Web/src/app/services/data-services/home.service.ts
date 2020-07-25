import { GroupSelect } from './../../models/GroupSelect';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { GroupTile } from 'src/app/models/GroupTile';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient) {

  }

  getUserGroups() {
    const url = `${environment.apiUrl}groups/GetUserGroups`;

    return this.http.get<GroupTile[]>(url);
  }

  getGroups(filter: string) {
    const url = `${environment.apiUrl}groups/Get?pageSize=5&page=1&filter=${filter}`;

    return this.http.get<GroupSelect[]>(url);
  }

  createGroup(name: string, description: string, isPrivate: boolean) {
    const url = `${environment.apiUrl}groups/Create`;

    return this.http.post<any>(url, { name, description, isPrivate });
  }
}
