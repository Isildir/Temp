import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Post } from '../interfaces/Post';
import { GroupDetails } from '../interfaces/GroupDetails';
import { GroupAdminDetails } from '../interfaces/GroupAdminDetails';
import { Comment } from '../interfaces/Comment';
import { Message } from '../interfaces/Message';
import { HttpRequestWrapperService } from 'src/app/core/services/http-request-wrapper.service';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  constructor(private httpRequestWrapper: HttpRequestWrapperService) { }

  getPosts(id: number, pageSize: number, page: number) {
    const url = `${environment.apiUrl}posts/get?groupId=${id}&page=${page}&pageSize=${pageSize}`;

    return this.httpRequestWrapper.get<Post[]>(url);
  }

  getDetails(id: number) {
    const url = `${environment.apiUrl}groups/Details?id=${id}`;

    return this.httpRequestWrapper.get<GroupDetails>(url);
  }

  addPost(groupId: number, content: string) {
    const url = `${environment.apiUrl}posts/Create`;

    return this.httpRequestWrapper.post<Post>(url, { groupId, content });
  }

  deletePost(id: number) {
    const url = `${environment.apiUrl}posts/Delete?id=${id}`;

    return this.httpRequestWrapper.deleteBoolean(url);
  }

  deleteComment(id: number) {
    const url = `${environment.apiUrl}comments/Delete?id=${id}`;

    return this.httpRequestWrapper.deleteBoolean(url);
  }

  addComment(id: number, content: string) {
    const url = `${environment.apiUrl}comments/Create`;

    return this.httpRequestWrapper.post<Comment>(url, { postId: id, content });
  }

  modifyPost(postId: number, content: string) {
    const url = `${environment.apiUrl}posts/Modify?id=${postId}`;

    return this.httpRequestWrapper.put<Post>(url, { content });
  }

  getAdminGroupDetails(groupId: number) {
    const url = `${environment.apiUrl}groups/GetAdminGroupDetails?id=${groupId}`;

    return this.httpRequestWrapper.get<GroupAdminDetails>(url);
  }

  resolveApplication(groupId: number, userId: number, accepted: boolean) {
    const url = `${environment.apiUrl}groups/ResolveApplication`;

    return this.httpRequestWrapper.postBoolean(url, { groupId, userId, accepted });
  }

  inviteUser(groupId: number, userIdentifier: string) {
    const url = `${environment.apiUrl}groups/InviteUser`;

    return this.httpRequestWrapper.postBoolean(url, { groupId, userIdentifier });
  }

  loadComments(groupId: number) {
    const url = `${environment.apiUrl}messages/Get?groupId=${groupId}&page=1&pageSize=10`;

    return this.httpRequestWrapper.get<Message[]>(url);
  }
}