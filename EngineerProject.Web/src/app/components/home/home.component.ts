import { GroupSelect } from './../../models/GroupSelect';
import { GroupCreateDialogComponent } from './group-create-dialog/group-create-dialog.component';
import { HomeService } from './../../services/data-services/home.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, debounceTime, switchMap } from 'rxjs/operators';

import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GroupTile } from 'src/app/models/GroupTile';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  public stateCtrl = new FormControl();
  public filteredGroups: Observable<GroupSelect[]>;
  public participantGroups: GroupTile[];
  public invitedGroups: GroupTile[];
  public awaitingGroups: GroupTile[];
  public rejectedGroups: GroupTile[];

  constructor(
    private router: Router,
    private homeService: HomeService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar
    ) {
      this.filteredGroups = this.stateCtrl.valueChanges
        .pipe(
          startWith(''),
          debounceTime(200),
          switchMap(filter => this.homeService.getGroups(filter || ''))
        );

      this.reloadGroupTiles();
    }

  ngOnInit() {
  }

  reloadGroupTiles() {
    this.homeService.getUserGroups().subscribe(data => {
      this.participantGroups = data.filter(a => a.state === 1 || a.state === 2);
      this.invitedGroups = data.filter(a => a.state === 3);
      this.awaitingGroups = data.filter(a => a.state === 4);
      this.rejectedGroups = data.filter(a => a.state === 5);
    });
  }

  onGroupSelect(id: number) {
    this.router.navigate([`/groups/${id}`]);
  }

  openClientCreationDialog() {
    const dialogRef = this.dialog.open(GroupCreateDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        this.reloadGroupTiles();
        this.snackBar.open(result, 'Ok', { duration: 5000 });
      }
    });
  }
}
