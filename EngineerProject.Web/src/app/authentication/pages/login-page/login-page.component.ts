import { SharedDataService } from '../../../shared/services/shared-data.service';
import { LoginMode } from '../../enums/LoginMode.enum';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GenericFormBuilderService } from 'src/app/shared/services/generic-form-builder.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.sass']
})
export class LoginPageComponent implements OnInit {
  public LoginMode = LoginMode;
  public loginForm: FormGroup;
  public loading = false;
  public returnUrl: string;
  public loginMode = LoginMode.Login;

  constructor(
    private formBuilder: GenericFormBuilderService,
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
    this.loginForm = this.formBuilder.createForm([
      { name: 'identifier' },
      { name: 'login' },
      { name: 'email' },
      { name: 'password' },
      { name: 'confirmedPassword' }
    ]);

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
    const values = this.formBuilder.getValues(this.loginForm, ['identifier', 'login', 'email', 'password', 'confirmedPassword']);

    switch (this.loginMode) {
      case LoginMode.Recovery:
        this.authenticationService.sendPasswordRecovery(values.identifier)
          .subscribe(() => {
            this.onReturnModeChange();
            this.snackBar.open('Email z nowym hasłem został wysłany', 'Ok', { duration: 5000 });
          },
          error => this.snackBar.open(error, 'Ok', { duration: 30000 }));
        break;
      case LoginMode.Register:
        if (values.password !== values.confirmedPassword) {
          this.snackBar.open('Podane hasła różnią się', 'Ok', { duration: 10000 });
          break;
        }

        this.authenticationService.register(values.login, values.email, values.password)
          .subscribe(() => {
            this.onReturnModeChange();
            this.snackBar.open('Rejestracja się powiodła, zaloguj się', 'Ok', { duration: 5000 });
          },
          error => this.snackBar.open(error, 'Ok', { duration: 30000 }));
        break;
      case LoginMode.Login:
        this.authenticationService.login(values.identifier, values.password)
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