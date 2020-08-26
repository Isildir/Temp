import { SharedDataService } from '../../../shared/services/shared-data.service';
import { LoginMode } from '../../enums/LoginMode.enum';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  public LoginMode = LoginMode;
  public loginForm: FormGroup;
  public loading = false;
  public returnUrl: string;
  public loginMode = LoginMode.Login;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: UserService,
    private sharedData: SharedDataService,
    private snackBar: MatSnackBar
    ) {
      if (this.sharedData.userSubject.value) {
        this.router.navigate(['/groupsMenu']);
      }
    }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      identifier: [''],
      login: [''],
      email: [''],
      password: [''],
      confirmedPassword: ['']
    });

    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }

  onPasswordRecoveryMode() {
    this.loginMode = LoginMode.Recovery;
  }

  onRegisterModeChange() {
    this.loginMode = LoginMode.Register;
  }

  onReturnModeChange() {
    this.loginMode = LoginMode.Login;
  }

  onSubmit() {
    const identifier = this.loginForm.controls.identifier.value;
    const login = this.loginForm.controls.login.value;
    const email = this.loginForm.controls.email.value;
    const password = this.loginForm.controls.password.value;
    const confirmedPassword = this.loginForm.controls.confirmedPassword.value;

    switch (this.loginMode) {
      case LoginMode.Recovery:
        this.authenticationService.sendPasswordRecovery(identifier)
          .subscribe(() => {
            this.onReturnModeChange();
            this.snackBar.open('Email z nowym hasłem został wysłany', 'Ok', { duration: 5000 });
          },
          error => this.snackBar.open(error, 'Ok', { duration: 30000 }));
        break;
      case LoginMode.Register:
        if (password !== confirmedPassword) {
          this.snackBar.open('Podane hasła różnią się', 'Ok', { duration: 10000 });
          break;
        }

        this.authenticationService.register(login, email, password)
          .subscribe(() => {
            this.onReturnModeChange();
            this.snackBar.open('Rejestracja się powiodła, zaloguj się', 'Ok', { duration: 5000 });
          },
          error => this.snackBar.open(error, 'Ok', { duration: 30000 }));
        break;
      case LoginMode.Login:
        this.authenticationService.login(identifier, password)
          .subscribe(() => {
            this.router.navigate([this.returnUrl]);
          },
          error => {
            this.snackBar.open(error, 'Ok', { duration: 30000 });
            this.loading = false;
          });
        break;
    }
  }
}