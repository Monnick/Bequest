import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { AuthenticationService } from "../../services/authentication.service";
import { AlertService } from "../../services/alert.service";
import { Unauthorised } from "../../errors/unauthorised";
import { BasicComponent } from '../basic.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent extends BasicComponent implements OnInit {

  model: any = {};
  returnUrl: string;

  constructor(
    alertService: AlertService,
    private router: Router,
    private authenticationService: AuthenticationService
    ) {
      super(alertService);
    }

  ngOnInit() {
    // reset login status
    this.authenticationService.logout();
  }

  authenticate() {
    this.loading = true;
    this.authenticationService.login(this.model.login, this.model.password)
      .subscribe(
        data => {
          this.router.navigate(['/myprojects']);
        },
        error => {
          if(error instanceof Unauthorised)
            this.alertService.error('Login or password is incorrect');
          else
            this.handleError(error);
        });
  }
}
