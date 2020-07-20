import { MainPageDataService } from './../../services/main-page-data/main-page-data.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  newGroupForm = new FormGroup({
    name: new FormControl('')
  });

  constructor(private mainPageDataService: MainPageDataService) {
  }

  ngOnInit() {
  }

  async onGroupChange(id: number) {
    await this.mainPageDataService.setActiveGroup(id);
  }

  async leaveGroup(id: number) {
    await this.mainPageDataService.leaveGroup(id);
  }

  async createGroup() {
    await this.mainPageDataService.createGroup(this.newGroupForm.value.name);

    this.newGroupForm.reset();
  }
}
