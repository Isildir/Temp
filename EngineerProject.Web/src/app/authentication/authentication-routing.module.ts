import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { PasswordRecoveryPageComponent } from './pages/password-recovery-page/password-recovery-page.component';

const routes: Routes = [
  { path: 'authentication', component: LoginPageComponent },
  { path: 'authentication/login', component: LoginPageComponent },
  { path: 'authentication/recovery/:code', component: PasswordRecoveryPageComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class AuthenticationRoutingModule { }



