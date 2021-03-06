import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormGroup } from '@angular/forms';
import { HomeService } from '../../services/home.service';
import { GenericFormBuilderService } from 'src/app/shared/services/generic-form-builder.service';

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
    private formBuilder: GenericFormBuilderService,
    private homeService: HomeService) { }

  ngOnInit() {
    this.groupForm = this.formBuilder.createForm([
      { name: 'name', isRequired: true },
      { name: 'description' },
      { name: 'isPrivate' },
    ]);
  }

  onSubmit() {
    this.errors = [];

    const values = this.formBuilder.getValues(this.groupForm, ['name', 'description', 'isPrivate']);

    if (values.name.length < 4 || values.name.length > 50) {
      this.errors.push('Nazwa musi zawierać 4-50 znaków');
    }

    if (this.errors.length > 0) {
      return;
    }

    this.homeService
    .createGroup(name, values.description, values.isPrivate)
    .subscribe(
      () => this.thisDialogRef.close('Grupa została dodana'),
      error => this.errors.push(error)
    );
  }
}