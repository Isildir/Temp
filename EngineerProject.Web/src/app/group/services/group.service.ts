import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Post } from '../interfaces/Post';
import { GroupDetails } from '../interfaces/GroupDetails';
import { GroupAdminDetails } from '../interfaces/GroupAdminDetails';
import { Message } from '../interfaces/Message';
import { ApiServiceHandlerService } from 'src/app/core/services/api-service-handler.service';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  constructor(private httpClient: HttpClient, private t: ApiServiceHandlerService) { }

  getPosts(id: number, pageSize: number, page: number) {
    const url = `${environment.apiUrl}posts/get?groupId=${id}&page=${page}&pageSize=${pageSize}`;

    this.t.get(url).subscribe(result => console.log(result));
    return this.httpClient.get<Post[]>(url);
  }

  getDetails(id: number) {
    const url = `${environment.apiUrl}groups/Details?id=${id}`;

    return this.httpClient.get<GroupDetails>(url);
  }

  addPost(groupId: number, content: string) {
    const url = `${environment.apiUrl}posts/Create`;

    return this.httpClient.post<any>(url, { groupId, content });
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

  modifyPost(postId: number, content: string) {
    const url = `${environment.apiUrl}posts/Modify?id=${postId}`;

    return this.httpClient.put<any>(url, { content });
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

  loadComments(groupId: number) {
    const url = `${environment.apiUrl}messages/Get?groupId=${groupId}&page=1&pageSize=10`;

    return this.httpClient.get<Message[]>(url);
  }
}