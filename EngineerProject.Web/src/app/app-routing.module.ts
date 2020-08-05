import { ErrorComponent } from './components/error/error.component';
import { GroupComponent } from './components/group/group.component';
import { PasswordRecoveryComponent } from './components/password-recovery/password-recovery.component';
import { ProfileComponent } from './components/profile/profile.component';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './services/utility/auth-guard.service';
import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'group/:id', component: GroupComponent, canActivate: [AuthGuard] },
  { path: 'error', component: ErrorComponent },
  { path: 'login', component: LoginComponent },
  { path: 'recovery/:code', component: PasswordRecoveryComponent },

  { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }



