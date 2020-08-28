import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Post } from '../../interfaces/Post';
import { GroupService } from '../../services/group.service';
import { GenericFormBuilderService } from 'src/app/shared/services/generic-form-builder.service';

@Component({
  selector: 'app-post-tile',
  templateUrl: './post-tile.component.html',
  styleUrls: ['./post-tile.component.sass']
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
    private formBuilder: GenericFormBuilderService) { }

  ngOnInit() {
    this.commentForm = this.formBuilder.createForm([
      { name: 'content', isRequired: true }
    ]);

    this.modifyPostForm = this.formBuilder.createForm([
      { name: 'content', isRequired: true, value: this.data.content }
    ]);
  }

  addComment() {
    const content = this.formBuilder.getValue(this.commentForm, 'content');

    this.groupService.addComment(this.data.id, content)
      .subscribe(
        data => this.data.comments.push(data.data),
        () => this.parentSnackBar.emit('Wystąpił błąd'));
  }

  setModifyFlag() {
    this.isModified = true;
  }

  resetPostValues() {
    this.isModified = false;
    this.modifyPostForm.controls.content.setValue(this.data.content);
  }

  modifyPost() {
    const content = this.formBuilder.getValue(this.modifyPostForm, 'content');

    this.groupService.modifyPost(this.data.id, content)
      .subscribe(response => {
        this.data.content = content;
        this.data.editDate = response.data.editDate;
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