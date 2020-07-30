import { Post } from './../../models/Post';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { GroupDetails } from 'src/app/models/GroupDetails';
import { GroupAdminDetails } from 'src/app/models/GroupAdminDetails';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private httpClient: HttpClient) { }

  getPosts(id: number) {
    const url = `${environment.apiUrl}posts/get?groupId=${id}&page=1&pageSize=10`;

    return this.httpClient.get<Post[]>(url);
  }

  getDetails(id: number) {
    const url = `${environment.apiUrl}groups/Details?id=${id}`;

    return this.httpClient.get<GroupDetails>(url);
  }

  addPost(groupId: number, title: string, content: string) {
    const url = `${environment.apiUrl}posts/Create`;

    return this.httpClient.post<any>(url, { groupId, title, content });
  }

  deletePost(id: number) {
    const url = `${environment.apiUrl}posts/Delete?id=${id}`;

    return this.httpClient.delete<any>(url);
  }

  deleteComment(id: number) {
    const url = `${environment.apiUrl}comments/Delete?id=${id}`;

    return this.httpClient.delete<any>(url);
  }

  addComment(id: number, content: string) {
    const url = `${environment.apiUrl}comments/Create`;

    return this.httpClient.post<any>(url, { postId: id, content });
  }

  modifyPost(postId: number, title: string, content: string) {
    const url = `${environment.apiUrl}posts/Modify?id=${postId}`;

    return this.httpClient.put<any>(url, { title, content });
  }

  getAdminGroupDetails(groupId: number) {
    const url = `${environment.apiUrl}groups/GetAdminGroupDetails?id=${groupId}`;

    return this.httpClient.get<GroupAdminDetails>(url);
  }

  resolveApplication(groupId: number, userId: number, accepted: boolean) {
    const url = `${environment.apiUrl}groups/ResolveApplication`;

    return this.httpClient.post<any>(url, { groupId, userId, accepted });
  }

  inviteUser(groupId: number, userIdentifier: string) {
    const url = `${environment.apiUrl}groups/InviteUser`;

    return this.httpClient.post<any>(url, { groupId, userIdentifier });
  }
}
