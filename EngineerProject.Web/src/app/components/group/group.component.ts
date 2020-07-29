import { GroupDetails } from 'src/app/models/GroupDetails';
import { Observable, BehaviorSubject } from 'rxjs';
import { GroupService } from './../../services/data-services/group.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/models/Post';
import { MatDialog } from '@angular/material/dialog';
import { RequiredValidator, FormBuilder, FormGroup } from '@angular/forms';
import { GroupDetailsDialogComponent } from './group-details-dialog/group-details-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
//  private groupId: BehaviorSubject<number>;
//  this.groupId = new BehaviorSubject<number>(undefined);
//  this.groupId.subscribe(value => this.loadData(value));
//  this.groupId.next(params.id));

  public details: GroupDetails;
  public posts: Post[];
  public postForm: FormGroup;

  private groupId: number;

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

