import { Group } from 'src/app/models/Group';
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

    return this.http.get<Group[]>(url);
  }
}
