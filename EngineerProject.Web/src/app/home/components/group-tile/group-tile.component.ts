import { GroupTile } from '../../interfaces/GroupTile';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-group-tile',
  templateUrl: './group-tile.component.html',
  styleUrls: ['./group-tile.component.css']
})
export class GroupTileComponent implements OnInit {
  @Input() data: GroupTile;

  constructor() { }

  ngOnInit() {
  }

}
