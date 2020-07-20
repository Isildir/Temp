import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authorization/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
// fake komponent, tylko zeby bylo cos w routingu
export class HomeComponent implements OnInit {

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
    ) {
      if (!this.authenticationService.currentUserValue) {
        this.router.navigate(['/login']);
      }
    }

  ngOnInit() {
  }
}
