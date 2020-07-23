import { UserProfileData } from './models/UserProfileData';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from './services/data-services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  currentUser: UserProfileData;

  constructor(
    private router: Router,
    private authenticationService: UserService
  ) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }
}
