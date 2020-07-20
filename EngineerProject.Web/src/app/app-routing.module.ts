import { PasswordRecoveryComponent } from './components/password-recovery/password-recovery.component';
import { ProfilePageComponent } from './components/profile-page/profile-page.component';
import { Routes, RouterModule } from '@angular/router';

import { LoginPageComponent } from './components/login-page/login-page.component';
import { AuthGuard } from './services/authorization/auth-guard.service';
import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home-page/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  //{ path: 'clients/:id', component: ClientComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: ProfilePageComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginPageComponent },
  { path: 'recovery/:code', component: PasswordRecoveryComponent },

  { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }



