import { HomeService } from './../../services/data-services/home.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/data-services/user.service';
import { FormControl } from '@angular/forms';
import { Observable, BehaviorSubject } from 'rxjs';
import { map, startWith, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { Group } from 'src/app/models/Group';

//import { ClientAddDialogComponent } from './client-add-dialog/client-add-dialog.component';
import { ViewChild, AfterViewInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
//import { ClientsDataService } from 'src/app/services/data/clients-data.service';
import { MatSnackBar } from '@angular/material/snack-bar';
//import { AuthenticationService } from 'src/app/services/authorization/authentication.service';
//import { ClientsDataSource } from 'src/app/utility/ClientsDataSource';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { merge } from 'rxjs';
import { tap } from 'rxjs/operators';
import { GroupTile } from 'src/app/models/GroupTile';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  public stateCtrl = new FormControl();
  public filteredGroups: Observable<Group[]>;
  public userGroups: GroupTile[];

  constructor(
    private router: Router,
    private homeService: HomeService,
    public dialog: MatDialog
    ) {
      this.filteredGroups = this.stateCtrl.valueChanges
        .pipe(
          startWith(''),
          debounceTime(200),
          switchMap(filter => this.homeService.getGroups(filter || ''))
        );

      this.homeService.getUserGroups().subscribe(data => this.userGroups = data);
    }

  ngOnInit() {
  }

  onGroupSelect(id: number) {
    console.log("lel");
  }

  openClientCreationDialog() {
    /*const dialogRef = this.dialog.open(ClientAddDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        this.loadPage();
        this.snackBar.open('Dostawca został dodany', 'Ok', { duration: 5000 });
      }
    });*/
  }
}
