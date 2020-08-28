import { SharedDataService } from 'src/app/shared/services/shared-data.service';
import { UserService } from 'src/app/authentication/services/user.service';
import { UserProfileData } from '../../interfaces/UserProfileData';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';

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
    private formBuilder: FormBuilder,
    private sharedData: SharedDataService) { }

  ngOnInit() {
    this.passwordChangeForm = this.formBuilder.group({
      password: ['', Validators.required],
      oldPassword: ['', Validators.required],
      confirmedPassword: ['', Validators.required]
    });

    this.userService.getProfile().subscribe(data => this.userData = data);
  }

  changeNotificationSettings() {
    this.userService.changeNotificationSettings()
      .subscribe(() => this.userData.receiveNotifications = !this.userData.receiveNotifications);
  }

  onPasswordChange() {
    const password = this.passwordChangeForm.controls.password.value;
    const confirmedPassword = this.passwordChangeForm.controls.confirmedPassword.value;
    const oldPassword = this.passwordChangeForm.controls.oldPassword.value;

    this.errors = [];

    if (!this.sharedData.passwordRegex.test(password) || !this.sharedData.passwordRegex.test(confirmedPassword)) {
      this.errors.push(this.sharedData.passwordRequirementsError);
    }

    if (password !== confirmedPassword) {
      this.errors.push(this.sharedData.differentPasswordError);
    }

    if (this.errors.length > 0) {
      return;
    }

    this.userService.changePassword(oldPassword, password)
    .subscribe(() => {
        this.passwordChangeForm.reset();
        this.snackBar.open('Hasło zostało zmienione', 'Ok', { duration: 5000 });
      },
      error => this.errors.push(error)
    );
  }
}