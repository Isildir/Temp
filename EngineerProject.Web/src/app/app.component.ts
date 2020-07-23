import { UserData } from './models/UserData';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from './services/data-services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  currentUser: UserData;

  constructor(
    private router: Router,
    private userService: UserService
  ) {
    this.userService.currentUser.subscribe(data => this.currentUser = data);
  }

  logout() {
    this.userService.logout();
    this.router.navigate(['/login']);
  }
}
