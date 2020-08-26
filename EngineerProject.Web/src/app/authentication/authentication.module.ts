import { SharedModule } from './../shared/shared.module';
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { NgModule } from '@angular/core';
import { PasswordRecoveryPageComponent } from './pages/password-recovery-page/password-recovery-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';

@NgModule({
   declarations: [
      LoginPageComponent,
      PasswordRecoveryPageComponent
   ],
   imports: [
      AuthenticationRoutingModule,
      SharedModule
   ]
})
export class AuthenticationModule { }