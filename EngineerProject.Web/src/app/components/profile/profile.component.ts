import { UserService } from 'src/app/services/data-services/user.service';
import { UserProfileData } from '../../models/UserProfileData';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { StaticValuesService } from 'src/app/services/sharedData/static-values.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['../../shared-styles/shared-styles-forms.css', '../../shared-styles/shared-styles.css', './profile.component.css']
})
export class ProfileComponent implements OnInit {
  public userData: UserProfileData;
  public passwordChangeForm: FormGroup;
  public errors: string[];

  constructor(
    private userService: UserService,
    private snackBar: MatSnackBar,
    private formBuilder: FormBuilder,
    private staticValues: StaticValuesService) { }

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

    if (!this.staticValues.passwordRegex.test(password) || !this.staticValues.passwordRegex.test(confirmedPassword)) {
      this.errors.push(this.staticValues.passwordRequirementsError);
    }

    if (password !== confirmedPassword) {
      this.errors.push(this.staticValues.differentPasswordError);
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
