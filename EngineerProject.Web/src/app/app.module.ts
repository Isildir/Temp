import { PasswordRecoveryComponent } from './components/password-recovery/password-recovery.component';
import { MatInputModule } from '@angular/material/input';
import { JwtInterceptor } from './services/utility/jwt-interceptor.service';
import { ErrorInterceptor } from './services/utility/error-interceptor.service';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { HomeComponent } from './components/home-page/home.component';
import { MatNativeDateModule } from '@angular/material/core';
import { ProfilePageComponent } from './components/profile-page/profile-page.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      LoginPageComponent,
      ProfilePageComponent,
      PasswordRecoveryComponent
   ],
   imports: [
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BrowserModule,
      AppRoutingModule,
      BrowserAnimationsModule,
      MatDialogModule,
      MatSnackBarModule,
      MatCheckboxModule,
      MatTableModule,
      MatSortModule,
      MatPaginatorModule,
      MatProgressSpinnerModule,
      MatSelectModule,
      MatFormFieldModule,
      MatDatepickerModule,
      MatInputModule,
      MatNativeDateModule
   ],
   providers: [
      { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
