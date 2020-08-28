import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HomeService } from '../../services/home.service';

@Component({
  selector: 'app-group-create-dialog',
  templateUrl: './group-create-dialog.component.html',
  styleUrls: ['./group-create-dialog.component.sass']
})
export class GroupCreateDialogComponent implements OnInit {
  public groupForm: FormGroup;
  public errors: string[];

  constructor(
    public thisDialogRef: MatDialogRef<GroupCreateDialogComponent>,
    private formBuilder: FormBuilder,
    private homeService: HomeService) { }

  ngOnInit() {
    this.groupForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      isPrivate: [false]
    });
  }

  onSubmit() {
    this.errors = [];

    const name = this.groupForm.controls.name.value;
    const description = this.groupForm.controls.description.value;
    const isPrivate = this.groupForm.controls.isPrivate.value;

    if (name.length < 4 || name.length > 50) {
      this.errors.push('Nazwa musi zawierać 4-50 znaków');
    }

    if (this.errors.length > 0) {
      return;
    }

    this.homeService
    .createGroup(name, description, isPrivate)
    .subscribe(
      () => this.thisDialogRef.close('Grupa została dodana'),
      error => this.errors.push(error)
    );
  }
}