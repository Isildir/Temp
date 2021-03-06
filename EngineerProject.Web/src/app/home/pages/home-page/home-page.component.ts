import { HomeService } from '../../services/home.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, debounceTime, switchMap, map } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GroupTile } from 'src/app/home/interfaces/GroupTile';
import { GroupCreateDialogComponent } from '../../components/group-create-dialog/group-create-dialog.component';
import { GenericRequestResult } from 'src/app/shared/interfaces/GenericRequestResult';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.sass']
})

export class HomePageComponent implements OnInit {
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
          switchMap(filter => this.homeService.getGroups(filter || '').pipe(map(data => data.data)))
        );

      this.reloadGroupTiles();
    }

  ngOnInit() {
  }

  reloadGroupTiles() {
    this.homeService.getUserGroups().subscribe(data => {
      this.participantGroups = data.data.participant;
      this.invitedGroups = data.data.invited;
      this.awaitingGroups = data.data.waiting;
    });

  }

  onGroupSelect(id: number) {
    this.router.navigate([`/group/overview/${id}`]);
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