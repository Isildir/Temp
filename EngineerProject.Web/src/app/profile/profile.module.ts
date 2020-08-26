import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { SharedModule } from '../shared/shared.module';
import { ProfileRoutingModule } from './profile-routing.module';
import { NgModule } from '@angular/core';

@NgModule({
   declarations: [
      ProfilePageComponent
   ],
   imports: [
      SharedModule,
      ProfileRoutingModule
   ]
})
export class ProfileModule { }