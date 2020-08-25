import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from './authentication/services/user.service';
import { SharedDataService } from './shared/services/shared-data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  userLogin: string;

  constructor(
    private router: Router,
    private userService: UserService,
    private sharedData: SharedDataService
  ) {
    this.sharedData.userData.subscribe(data => this.userLogin = data?.login);
  }

  logout() {
    this.userService.logout();
    this.router.navigate(['/authentication/login']);
  }
}
