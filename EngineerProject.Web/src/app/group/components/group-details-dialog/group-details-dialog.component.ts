import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, RequiredValidator } from '@angular/forms';
import { GroupAdminDetails } from '../../interfaces/GroupAdminDetails';
import { GroupService } from '../../services/group.service';
import { GenericFormBuilderService } from 'src/app/shared/services/generic-form-builder.service';

@Component({
  selector: 'app-group-details-dialog',
  templateUrl: './group-details-dialog.component.html',
  styleUrls: ['./group-details-dialog.component.sass']
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
    private formBuilder: GenericFormBuilderService) {
      this.groupId = values.groupId;
    }

  ngOnInit() {
    this.inviteForm = this.formBuilder.createForm([
      { name: 'identifier', isRequired: true }
    ]);

    this.settingsForm = this.formBuilder.createForm([
      { name: 'name', isRequired: true },
      { name: 'description' },
      { name: 'isPrivate' },
    ]);

    this.groupService.getAdminGroupDetails(this.groupId).subscribe(data => {
      this.data = data.data;

      this.formBuilder.setValue(this.settingsForm, 'name', data.data.name);
      this.formBuilder.setValue(this.settingsForm, 'description', data.data.description);
      this.formBuilder.setValue(this.settingsForm, 'isPrivate', data.data.isPrivate);
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
    const identifier = this.formBuilder.getValue(this.inviteForm, 'identifier');

    this.groupService.inviteUser(this.groupId, identifier).subscribe(() => this.inviteForm.controls.identifier.setValue(''));
  }

  private removeCandidateFromData(id: number) {
    const candidate = this.data.candidates.find(a => a.userId === id);
    const index = this.data.candidates.indexOf(candidate);

    this.data.candidates.splice(index, 1);
  }
}