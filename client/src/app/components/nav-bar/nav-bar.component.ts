import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'nav-bar',
  templateUrl: './nav-bar.component.html'
})
export class NavBarComponent implements OnInit {

  isLoggedIn : boolean;
  currentAccountId : string;

  constructor(
    private authService : AuthenticationService,
    private router : Router
  ) { }

  ngOnInit() {
    this.authService.status().subscribe(status => {
      if(status.loggedIn)
        this.currentAccountId = status.id;
      else
        this.currentAccountId = '';

      this.isLoggedIn = status.loggedIn;
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
