import { GroupAdminDetails } from './../../../models/GroupAdminDetails';
import { GroupService } from './../../../services/data-services/group.service';
import { GroupDetails } from './../../../models/GroupDetails';
import { HomeService } from './../../../services/data-services/home.service';
import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators, RequiredValidator } from '@angular/forms';

@Component({
  selector: 'app-group-details-dialog',
  templateUrl: './group-details-dialog.component.html',
  styleUrls: ['./group-details-dialog.component.css']
})
export class GroupDetailsDialogComponent implements OnInit {
  public data: GroupAdminDetails;
  public inviteForm: FormGroup;
  public settingsForm: FormGroup;

  private groupId: number;

  constructor(
    public thisDialogRef: MatDialogRef<GroupDetailsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public values: any,
    private groupService: GroupService,
    private formBuilder: FormBuilder) {
      this.groupId = values.groupId;
    }

  ngOnInit() {
    this.inviteForm = this.formBuilder.group({
      identifier: ['', new RequiredValidator()]
    });

    this.groupService.getAdminGroupDetails(this.groupId).subscribe(data => {
      this.data = data;

      this.settingsForm = this.formBuilder.group({
        name: [data.name, new RequiredValidator()],
        description: [data.description, new RequiredValidator()],
        isPrivate: [data.isPrivate]
      });
    });
  }

  public changeSettings() {
    console.log(this.settingsForm);
  }

  public acceptCandidate(id: number) {
    this.groupService.resolveApplication(this.groupId, id, true).subscribe(() => this.removeCandidateFromData(id));
  }

  public rejectCandidate(id: number) {
    this.groupService.resolveApplication(this.groupId, id, false).subscribe(() => this.removeCandidateFromData(id));
  }

  public inviteUser() {
    const identifier = this.inviteForm.controls.identifier.value;

    this.groupService.inviteUser(this.groupId, identifier).subscribe(() => this.inviteForm.controls.identifier.setValue(''));
  }

  private removeCandidateFromData(id: number) {
    const candidate = this.data.candidates.find(a => a.userId === id);
    const index = this.data.candidates.indexOf(candidate);

    this.data.candidates.splice(index, 1);
  }
}
