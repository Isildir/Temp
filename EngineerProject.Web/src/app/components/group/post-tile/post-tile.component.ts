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

  constructor(
    private groupService: GroupService,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.commentForm = this.formBuilder.group({
      content: ['', new RequiredValidator()]
    });
  }

  addComment() {
    const content = this.commentForm.controls.content.value;

    this.groupService.addComment(this.data.id, content)
      .subscribe(
        data => this.data.comments.push(data),
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
