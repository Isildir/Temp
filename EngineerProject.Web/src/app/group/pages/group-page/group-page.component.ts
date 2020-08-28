import { WindowScrollService } from './../../../shared/services/window-scroll.service';
import { FilesManagerComponent } from './../../components/files-manager/files-manager.component';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { RequiredValidator, FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GroupDetails } from '../../interfaces/GroupDetails';
import { Post } from '../../interfaces/Post';
import { ChatComponent } from '../../components/chat/chat.component';
import { GroupService } from '../../services/group.service';
import { GroupDetailsDialogComponent } from '../../components/group-details-dialog/group-details-dialog.component';
import { GenericFormBuilderService } from 'src/app/shared/services/generic-form-builder.service';

@Component({
  selector: 'app-group-page',
  templateUrl: './group-page.component.html',
  styleUrls: ['./group-page.component.sass']
})
export class GroupPageComponent implements OnInit, AfterViewInit {
  public details: GroupDetails;
  public posts = Array<Post>();
  public postForm: FormGroup;

  private groupId: number;
  private postsLoaded = 0;
  private pageSize = 10;
  private dataLoading = false;
  private dataEndReached = false;

  @ViewChild(ChatComponent) chat: ChatComponent;
  @ViewChild(FilesManagerComponent) files: FilesManagerComponent;

  constructor(
    private activatedRoute: ActivatedRoute,
    private groupService: GroupService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private formBuilder: GenericFormBuilderService,
    scrollService: WindowScrollService) {
      scrollService.contentScrolledPercentage.subscribe(value => {
        if (!this.dataEndReached && !this.dataLoading && value >= 0.9) {
          this.loadPosts();
        }
      });
    }

  ngOnInit() {
    this.postForm = this.formBuilder.createForm([{ name: 'content', isRequired: true }]);

    this.activatedRoute.params.subscribe(params => {
      this.groupId = +params.id;
      this.loadPosts();
      this.loadGroupDetails();
    });
  }

  ngAfterViewInit() {
    this.chat.setComponentData(this.groupId);
    this.files.setComponentData(this.groupId);
  }

  loadGroupDetails() {
    this.groupService.getDetails(this.groupId).subscribe(data => this.details = data);
  }

  loadPosts() {
    this.dataLoading = true;
    this.groupService.getPosts(this.groupId, this.pageSize, Math.floor(this.postsLoaded / 10) + 1).subscribe(data => {
      this.posts.push(...data);
      this.postsLoaded += data.length;

      if (data.length < this.pageSize) {
        this.dataEndReached = true;
      }

      this.dataLoading = false;
    });
  }

  parentSnackBar(value: string) {
    this.snackBar.open(value, 'Ok', { duration: 5000 });
  }

  openDetailsDialog() {
    const dialogRef = this.dialog.open(GroupDetailsDialogComponent, { data: { groupId: this.groupId }});
    dialogRef.afterClosed().subscribe(() => {
      this.loadGroupDetails();
    });
  }

  addPost() {
    const content = this.formBuilder.getValue(this.postForm, 'content');

    this.groupService.addPost(this.groupId, content)
      .subscribe(() => {
        this.posts = [];
        this.postsLoaded = 0;
        this.loadPosts();
        this.snackBar.open('', 'Ok', { duration: 5000 });
      },
      error => this.snackBar.open(error, 'Ok', { duration: 30000 }));
  }
}