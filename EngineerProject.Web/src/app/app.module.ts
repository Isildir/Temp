import { ErrorComponent } from './components/error/error.component';
import { ChatComponent } from './components/group/chat/chat.component';
import { FilesManagerComponent } from './components/files-manager/files-manager.component';
import { GroupDetailsDialogComponent } from './components/group/group-details-dialog/group-details-dialog.component';
import { PostTileComponent } from './components/group/post-tile/post-tile.component';
import { GroupComponent } from './components/group/group.component';
import { GroupCreateDialogComponent } from './components/home/group-create-dialog/group-create-dialog.component';
import { GroupTileComponent } from './components/home/group-tile/group-tile.component';
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
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { MatNativeDateModule } from '@angular/material/core';
import { ProfileComponent } from './components/profile/profile.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      LoginComponent,
      ProfileComponent,
      PasswordRecoveryComponent,
      GroupTileComponent,
      GroupCreateDialogComponent,
      GroupComponent,
      PostTileComponent,
      GroupDetailsDialogComponent,
      FilesManagerComponent,
      ChatComponent,
      ErrorComponent
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
      MatNativeDateModule,
      MatAutocompleteModule,
      BrowserAnimationsModule,
      MatButtonModule,
      MatToolbarModule,
      MatIconModule
   ],
   exports: [
      BrowserAnimationsModule,
      MatButtonModule,
      MatToolbarModule,
      MatIconModule
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
