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
  public filteredGroups: Observable<GroupTile[]>;
  public participantGroups: GroupTile[];
  public invitedGroups: GroupTile[];
  public awaitingGroups: GroupTile[];

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
      this.participantGroups = data.participant;
      this.invitedGroups = data.invited;
      this.awaitingGroups = data.waiting;
    });
  }

  onGroupSelect(id: number) {
    this.router.navigate([`/group/${id}`]);
  }

  joinGroup(id: number) {
    this.homeService.askForInvite(id).subscribe(() => {
      this.reloadGroupTiles();
      this.stateCtrl.reset();
    });
  }

  resolveGroupInvite(id: number, value: boolean) {
    this.homeService.resolveGroupInvite(id, value).subscribe(() => {
      this.reloadGroupTiles();
    });
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
