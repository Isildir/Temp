import { SharedDataService } from 'src/app/shared/services/shared-data.service';
import { UserService } from 'src/app/authentication/services/user.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-password-recovery-page',
  templateUrl: './password-recovery-page.component.html',
  styleUrls: ['../../../shared/styles/styles-forms.sass', './password-recovery-page.component.sass']
})
export class PasswordRecoveryPageComponent implements OnInit {
private recoveryCode: string;

public wrongCodeMessage: string;
public codeChecked: boolean;

public passwordRecoveryForm: FormGroup;
public errors: string[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private authenticationService: UserService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private sharedData: SharedDataService,
    private router: Router) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => this.recoveryCode = params.code);
    this.authenticationService
    .checkPasswordRecovery(this.recoveryCode)
    .subscribe(
      () => this.codeChecked = true,
      error => {
        this.wrongCodeMessage = error;
        this.codeChecked = true;
      }
    );

    this.passwordRecoveryForm = this.formBuilder.group({
      identifier: ['', Validators.required],
      password: ['', Validators.required],
      confirmedPassword: ['', Validators.required]
    });
  }

  onSubmit() {
    this.errors = [];

    const identifier = this.passwordRecoveryForm.controls.identifier.value;
    const password = this.passwordRecoveryForm.controls.password.value;
    const confirmedPassword = this.passwordRecoveryForm.controls.confirmedPassword.value;

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

    this.authenticationService.usePasswordRecovery(identifier, password, this.recoveryCode)
    .subscribe(
      () => {
        this.router.navigate(['/authentication/login']);
        this.snackBar.open('Hasło zostało zmienione, możesz się teraz zalogować', 'Ok', { duration: 5000 });
      },
      error => this.errors.push(error)
    );
  }

  onMoveToLogin() {
    this.router.navigate(['/authentication/login']);
  }
}