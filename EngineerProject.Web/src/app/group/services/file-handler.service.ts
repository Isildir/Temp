import { NewFile } from './../interfaces/NewFile';
import { GetFile } from './../interfaces/GetFile';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CustomHttpParams } from 'src/app/core/utility/customHttpParams';
import { HttpRequestWrapperService } from 'src/app/core/services/http-request-wrapper.service';

@Injectable({
  providedIn: 'root'
})
export class FileHandlerService {
constructor(private httpRequestWrapper: HttpRequestWrapperService) { }

  sendFile(file: File, groupId: number) {
    const formData = new FormData();
    formData.append(`file`, file, file.name);

    const url = `${environment.apiUrl}files/UploadFile?groupId=${groupId}`;

    return this.httpRequestWrapper.post<NewFile>(url, formData, { params: new CustomHttpParams(true) });
  }

  getFiles(groupId: number) {
    const url = `${environment.apiUrl}files/GetFiles?groupId=${groupId}&pageSize=5&page=1&filter=''`;

    return this.httpRequestWrapper.get<GetFile[]>(url);
  }

  deleteFile(id: number) {
    const url = `${environment.apiUrl}files/DeleteFile?id=${id}`;

    return this.httpRequestWrapper.deleteBoolean(url);
  }
}