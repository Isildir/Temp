import { SharedDataService } from 'src/app/shared/services/shared-data.service';
import { UserService } from 'src/app/authentication/services/user.service';
import { UserProfileData } from '../../interfaces/UserProfileData';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { GenericFormBuilderService } from 'src/app/shared/services/generic-form-builder.service';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['../../../shared/styles/styles-forms.sass', '../../../shared/styles/styles.sass', './profile-page.component.sass']
})
export class ProfilePageComponent implements OnInit {
  public userData: UserProfileData;
  public passwordChangeForm: FormGroup;
  public errors: string[];

  constructor(
    private userService: UserService,
    private snackBar: MatSnackBar,
    private formBuilder: GenericFormBuilderService,
    private sharedData: SharedDataService) { }

  ngOnInit() {
    this.passwordChangeForm = this.formBuilder.createForm([
      { name: 'password', isRequired: true },
      { name: 'oldPassword', isRequired: true },
      { name: 'confirmedPassword', isRequired: true },
    ]);

    this.userService.getProfile().subscribe(data => this.userData = data.data);
  }

  changeNotificationSettings() {
    this.userService.changeNotificationSettings()
      .subscribe(() => this.userData.receiveNotifications = !this.userData.receiveNotifications);
  }

  onPasswordChange() {
    const values = this.formBuilder.getValues(this.passwordChangeForm, ['password', 'confirmedPassword', 'oldPassword']);

    this.errors = [];

    if (!this.sharedData.passwordRegex.test(values.password) || !this.sharedData.passwordRegex.test(values.confirmedPassword)) {
      this.errors.push(this.sharedData.passwordRequirementsError);
    }

    if (values.password !== values.confirmedPassword) {
      this.errors.push(this.sharedData.differentPasswordError);
    }

    if (this.errors.length > 0) {
      return;
    }

    this.userService.changePassword(values.oldPassword, values.password)
    .subscribe(() => {
        this.passwordChangeForm.reset();
        this.snackBar.open('Hasło zostało zmienione', 'Ok', { duration: 5000 });
      },
      error => this.errors.push(error)
    );
  }
}