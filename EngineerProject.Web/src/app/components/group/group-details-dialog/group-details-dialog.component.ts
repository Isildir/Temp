import { GroupDetails } from './../../../models/GroupDetails';
import { HomeService } from './../../../services/data-services/home.service';
import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-group-details-dialog',
  templateUrl: './group-details-dialog.component.html',
  styleUrls: ['./group-details-dialog.component.css']
})
export class GroupDetailsDialogComponent implements OnInit {

  constructor(
    public thisDialogRef: MatDialogRef<GroupDetailsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public values: any) {
      console.log(values.groupId);
    }

  ngOnInit() {
    // this.thisDialogRef.close('Grupa została dodana')
  }
}
