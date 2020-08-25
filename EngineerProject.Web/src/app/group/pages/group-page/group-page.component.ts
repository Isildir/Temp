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

@Component({
  selector: 'app-group-page',
  templateUrl: './group-page.component.html',
  styleUrls: ['./group-page.component.css']
})
export class GroupPageComponent implements OnInit, AfterViewInit {
  public details: GroupDetails;
  public posts: Post[];
  public postForm: FormGroup;

  private groupId: number;

  @ViewChild(ChatComponent) chat: ChatComponent;

  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private groupService: GroupService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar) {
    }

  ngOnInit() {
    this.postForm = this.formBuilder.group({
      title: ['', new RequiredValidator()],
      content: ['', new RequiredValidator()]
    });

    this.activatedRoute.params.subscribe(params => {
      this.groupId = +params.id;
      this.reloadPosts();
      this.loadGroupDetails();
    });
  }

  ngAfterViewInit() {
    this.chat.setComponentData(this.groupId);
  }

  loadGroupDetails() {
    this.groupService.getDetails(this.groupId).subscribe(data => this.details = data);
  }

  reloadPosts() {
    this.groupService.getPosts(this.groupId).subscribe(data => this.posts = data);
  }

  parentSnackBar(value: string) {
    this.snackBar.open(value, 'Ok', { duration: 5000 });
  }

  openDetailsDialog() {
    const dialogRef = this.dialog.open(GroupDetailsDialogComponent, { data: { groupId: this.groupId }});
    dialogRef.afterClosed().subscribe(result => {
      this.loadGroupDetails();
    });
  }

  addPost() {
    const title = this.postForm.controls.title.value;
    const content = this.postForm.controls.content.value;

    this.groupService.addPost(this.groupId, title, content)
      .subscribe(() => {
        this.reloadPosts();
        this.snackBar.open('', 'Ok', { duration: 5000 });
      },
      error => this.snackBar.open(error, 'Ok', { duration: 30000 }));
  }
}

