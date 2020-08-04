import { NewFile } from './../../models/NewFile';
import { GetFile } from './../../models/GetFile';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CustomHttpParams } from '../utility/customHttpParams';

@Injectable({
  providedIn: 'root'
})
export class FileHandlerService {

constructor(private httpClient: HttpClient) { }

  sendFile(file: File, groupId: number) {
    const formData = new FormData();
    formData.append(`file`, file, file.name);

    const url = `${environment.apiUrl}files/UploadFile?groupId=${groupId}`;

    return this.httpClient.post<NewFile>(url, formData, { params: new CustomHttpParams(true) });
  }

  getFiles(groupId: number) {
    const url = `${environment.apiUrl}files/GetFiles?groupId=${groupId}`;

    return this.httpClient.get<GetFile[]>(url);
  }

  deleteFile(id: number) {
    const url = `${environment.apiUrl}files/DeleteFile?id=${id}`;

    return this.httpClient.delete<any>(url);
  }
}
