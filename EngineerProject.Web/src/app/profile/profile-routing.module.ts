import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../core/auth-guard/auth-guard.service';

const routes: Routes = [
  { path: 'profile', component: ProfilePageComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class ProfileRoutingModule { }



