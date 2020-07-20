import { AuthenticationService } from 'src/app/services/authorization/authentication.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { StaticValuesService } from 'src/app/services/sharedData/static-values.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-password-recovery',
  templateUrl: './password-recovery.component.html',
  styleUrls: ['../../shared-styles/shared-styles-forms.css', './password-recovery.component.css']
})
export class PasswordRecoveryComponent implements OnInit {

private recoveryCode: string;

public wrongCodeMessage: string;
public codeChecked: boolean;

public passwordRecoveryForm: FormGroup;
public errors: string[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private staticValues: StaticValuesService,
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
      login: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmedPassword: ['', Validators.required]
    });
  }

  onSubmit() {
    this.errors = [];

    const login = this.passwordRecoveryForm.controls.login.value;
    const email = this.passwordRecoveryForm.controls.email.value;
    const password = this.passwordRecoveryForm.controls.password.value;
    const confirmedPassword = this.passwordRecoveryForm.controls.confirmedPassword.value;

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

    this.authenticationService.usePasswordRecovery(login, email, password, this.recoveryCode)
    .subscribe(
      () => {
        this.router.navigate(['/login']);
        this.snackBar.open('Hasło zostało zmienione, możesz się teraz zalogować', 'Ok', { duration: 5000 });
      },
      error => this.errors.push(error)
    );
  }

  onMoveToLogin() {
    this.router.navigate(['/login']);
  }
}
