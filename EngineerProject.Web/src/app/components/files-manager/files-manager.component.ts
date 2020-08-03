import { GetFile } from './../../models/GetFile';
import { NewFile } from './../../models/NewFile';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FileHandlerService } from 'src/app/services/data-services/file-handler.service';

@Component({
  selector: 'app-files-manager',
  templateUrl: './files-manager.component.html',
  styleUrls: ['./files-manager.component.css']
})
export class FilesManagerComponent implements OnInit {
  @Input() groupId: number;
  @Output() parentSnackBar: EventEmitter<string> = new EventEmitter();

  public addedFiles = Array<NewFile>();
  public fileUploadActive = true;
  public filesToDownload: Array<GetFile>;

  public onFileDropped($event: any[]) {
    this.prepareFilesList($event);
  }

  public onFileUploadButtonClick() {
    this.fileUploadActive = true;
  }

  public onFileDownloadButtonClick() {
    this.fileHandlerService.getFiles(this.groupId).subscribe(data => {
      this.filesToDownload = data;
      this.fileUploadActive = false;
    }, error => console.log(error));
  }

  constructor(private fileHandlerService: FileHandlerService) {
  }

  ngOnInit() {}

  public fileBrowseHandler(files: any[]) {
    this.prepareFilesList(files);
  }

  public deleteFile(id: number) {
    this.fileHandlerService.deleteFile(id).subscribe(() => {
      const index: number = this.addedFiles.indexOf(this.addedFiles.find(a => a.id === id), 0);

      if (index > -1) {
        this.addedFiles.splice(index, 1);
      }
    }, error => this.parentSnackBar.emit(error));
  }

  public prepareFilesList(files: Array<any>) {
    for (const file of files) {
      this.fileHandlerService.sendFile(file, this.groupId).subscribe(result => {
        result.uploaded = true;
        result.size = file.size;

        this.addedFiles.push(result);
      }, error => {
        this.parentSnackBar.emit(error);

        const result = { errorMessage: error} as NewFile;

        result.fileName = file.name;
        result.uploaded = false;
        result.size = file.size;

        this.addedFiles.push(result);
      });
    }
  }

  public formatBytes(bytes: number, decimals: number) {
    if (bytes === 0) {
      return '0 Bytes';
    }

    const k = 1024;
    const dm = decimals <= 0 ? 0 : decimals || 2;
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));

    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
  }
}
