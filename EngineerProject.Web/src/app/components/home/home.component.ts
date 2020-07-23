import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/data-services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
// fake komponent, tylko zeby bylo cos w routingu
export class HomeComponent implements OnInit {

  constructor(
    private router: Router,
    private authenticationService: UserService
    ) {
      if (!this.authenticationService.currentUserValue) {
        this.router.navigate(['/login']);
      } else {
        this.router.navigate(['/groups']);
      }
    }

  ngOnInit() {
  }
}
