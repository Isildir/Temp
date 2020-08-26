import { NgModule } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatNativeDateModule } from '@angular/material/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { DatePipe, CommonModule } from '@angular/common';

@NgModule({
   imports: [
      CommonModule,
      ReactiveFormsModule,
      HttpClientModule,
      FormsModule,
      MatFormFieldModule,
      MatAutocompleteModule,
      MatDialogModule,
      MatSnackBarModule,
      MatCheckboxModule,
      MatTableModule,
      MatSortModule,
      MatPaginatorModule,
      MatProgressSpinnerModule,
      MatSelectModule,
      MatDatepickerModule,
      MatInputModule,
      MatNativeDateModule,
      MatButtonModule,
      MatToolbarModule,
      MatIconModule
   ],
   exports: [
      CommonModule,
      ReactiveFormsModule,
      HttpClientModule,
      FormsModule,
      MatFormFieldModule,
      MatAutocompleteModule,
      MatDialogModule,
      MatSnackBarModule,
      MatCheckboxModule,
      MatTableModule,
      MatSortModule,
      MatPaginatorModule,
      MatProgressSpinnerModule,
      MatSelectModule,
      MatDatepickerModule,
      MatInputModule,
      MatNativeDateModule,
      MatButtonModule,
      MatToolbarModule,
      MatIconModule
   ]
})
export class SharedModule { }