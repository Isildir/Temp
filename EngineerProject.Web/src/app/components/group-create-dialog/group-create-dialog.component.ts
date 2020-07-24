import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-group-create-dialog',
  templateUrl: './group-create-dialog.component.html',
  styleUrls: ['./group-create-dialog.component.css']
})
export class GroupCreateDialogComponent implements OnInit {
  public newClientForm: FormGroup;
  public errors: string[];

  constructor(
    //public thisDialogRef: MatDialogRef<ClientAddDialogComponent>,
    private formBuilder: FormBuilder,
    /*private clientsService: ClientsDataService*/) { }

  ngOnInit() {
    this.newClientForm = this.formBuilder.group({
      shortName: ['', Validators.required],
      fullName: ['', Validators.required]
    });
  }

  onSubmit() {
    this.errors = [];

    const shortName = this.newClientForm.controls.shortName.value;
    const fullName = this.newClientForm.controls.fullName.value;

    if (shortName.length < 2 || shortName.length > 12) {
      this.errors.push('Login musi zawierać 2-12 znaków');
    }

    if (fullName.length < 6 || fullName.length > 30) {
      this.errors.push('Nazwa musi zawierać 6-30 znaków');
    }

    if (this.errors.length > 0) {
      return;
    }
/*
    const client = new Client();

    client.ShortName = shortName;
    client.FullName = fullName;

    this.clientsService
    .addClient(client)
    .subscribe(
      () => this.thisDialogRef.close('Dostawca został dodany'),
      error => this.errors.push(error)
    );*/
  }
}
