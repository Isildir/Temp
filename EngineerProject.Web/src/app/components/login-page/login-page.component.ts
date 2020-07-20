import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../services/authorization/authentication.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  public loginForm: FormGroup;
  public loading = false;
  public returnUrl: string;
  public loginMode = true;
  public registerMode = false;
  public recoveryMode = false;

    constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private authenticationService: AuthenticationService,
      private snackBar: MatSnackBar
      ) {
        if (this.authenticationService.currentUserValue) {
          /*if (this.authenticationService.currentUserValue.IsAdmin) {
            this.router.navigate(['/clients']);
          } else {
            this.router.navigate(['/files']);
          }*/
        }
      }

    ngOnInit() {
      this.loginForm = this.formBuilder.group({
        login: ['', Validators.required],
        password: [''],
        confirmedPassword: ['']
      });

      this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
    }

    onPasswordRecoveryMode() {
      this.recoveryMode = !this.recoveryMode;
      this.loginMode = !this.recoveryMode;
    }

    onRegisterModeChange() {
      this.registerMode = !this.registerMode;
      this.loginMode = !this.registerMode;
    }

    resetModes() {
      this.loginMode = true;
      this.recoveryMode = false;
      this.registerMode = false;
    }

    onSubmit() {
      if (this.loginForm.invalid) {
        return;
      }

      const userName = this.loginForm.controls.login.value;

      if (this.recoveryMode) {
        this.authenticationService.sendPasswordRecovery(userName)
        .subscribe(() => {
          this.resetModes();
          this.snackBar.open('Email z nowym hasłem został wysłany', 'Ok', { duration: 5000 });
          },
          error => this.snackBar.open(error, 'Ok', { duration: 30000 }));
      } else if (this.registerMode) {
        const password = this.loginForm.controls.password.value;
        const confirmedPassword = this.loginForm.controls.confirmedPassword.value;

        if (password !== confirmedPassword) {
          this.snackBar.open('Podane hasła różnią się', 'Ok', { duration: 10000 });
          return;
        }

        this.authenticationService.register(userName, password)
        .subscribe(() => {
          this.resetModes();
          this.snackBar.open('Rejestracja się powiodła, zaloguj się', 'Ok', { duration: 5000 });
          },
          error => this.snackBar.open(error, 'Ok', { duration: 30000 }));
      } else if (this.loginMode) {
        const password = this.loginForm.controls.password.value;

        this.authenticationService.login(userName, password)
        .pipe(first())
        .subscribe(
            () => {
              this.router.navigate([this.returnUrl]);
            },
            error => {
              this.snackBar.open(error.error, 'Ok', { duration: 30000 });
              this.loading = false;
            });
      }
    }
}
