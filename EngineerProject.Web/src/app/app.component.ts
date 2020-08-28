import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from './authentication/services/user.service';
import { SharedDataService } from './shared/services/shared-data.service';
import { WindowScrollService } from './shared/services/window-scroll.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  public userLogin: string;

  @HostListener('window:scroll', ['$event']) onScroll(e: Event): void {
    const position = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    const maximumHeight = document.documentElement.scrollHeight;

    this.windowScrollService.contentScrolledPercentage.next(position / maximumHeight);
  }

  constructor(
    private router: Router,
    private userService: UserService,
    private sharedData: SharedDataService,
    private windowScrollService: WindowScrollService
  ) {
    this.sharedData.userData.subscribe(data => this.userLogin = data?.login);
  }

  logout() {
    this.userService.logout();
    this.router.navigate(['/authentication/login']);
  }
}