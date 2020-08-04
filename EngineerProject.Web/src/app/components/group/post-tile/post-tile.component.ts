import { GroupService } from './../../../services/data-services/group.service';
import { Component, OnInit, Input, Output, EventEmitter, ɵConsole } from '@angular/core';
import { Post } from 'src/app/models/Post';
import { FormBuilder, FormGroup, RequiredValidator } from '@angular/forms';

@Component({
  selector: 'app-post-tile',
  templateUrl: './post-tile.component.html',
  styleUrls: ['./post-tile.component.css']
})
export class PostTileComponent implements OnInit {
  @Input() data: Post;
  @Output() reloadPosts: EventEmitter<any> = new EventEmitter();
  @Output() parentSnackBar: EventEmitter<string> = new EventEmitter();

  public commentForm: FormGroup;
  public modifyPostForm: FormGroup;
  public isModified = false;

  constructor(
    private groupService: GroupService,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.commentForm = this.formBuilder.group({
      content: ['', new RequiredValidator()]
    });

    this.modifyPostForm = this.formBuilder.group({
      title: [this.data.title, new RequiredValidator()],
      content: [this.data.content, new RequiredValidator()]
    });
  }

  addComment() {
    const content = this.commentForm.controls.content.value;

    this.groupService.addComment(this.data.id, content)
      .subscribe(
        data => this.data.comments.push(data),
        () => this.parentSnackBar.emit('Wystąpił błąd'));
  }

  setModifyFlag() {
    this.isModified = true;
  }

  resetPostValues() {
    this.isModified = false;
    this.modifyPostForm.controls.title.setValue(this.data.title);
    this.modifyPostForm.controls.content.setValue(this.data.content);
  }

  modifyPost() {
    const title = this.modifyPostForm.controls.title.value;
    const content = this.modifyPostForm.controls.content.value;

    this.groupService.modifyPost(this.data.id, title, content)
      .subscribe(response => {
        this.data.title = title;
        this.data.content = content;
        this.data.editDate = response.editDate;
        this.resetPostValues();
      },
      () => this.parentSnackBar.emit('Wystąpił błąd'));
  }

  deleteComment(id: number) {
    this.groupService.deleteComment(id)
    .subscribe(
      () => this.reloadPosts.emit(),
      () => this.parentSnackBar.emit('Wystąpił błąd'));
  }

  deletePost() {
    this.groupService.deletePost(this.data.id)
    .subscribe(
      () => this.reloadPosts.emit(),
      () => this.parentSnackBar.emit('Wystąpił błąd'));
  }
}