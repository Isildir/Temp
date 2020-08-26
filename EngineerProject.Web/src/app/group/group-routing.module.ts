import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../core/auth-guard/auth-guard.service';
import { GroupPageComponent } from './pages/group-page/group-page.component';
import { NgModule } from '@angular/core';

const routes: Routes = [
  { path: 'overview/:id', component: GroupPageComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GroupRoutingModule { }
