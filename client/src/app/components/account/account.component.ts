import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { AlertService } from '../../services/alert.service';
import { Account } from '../../models/dtos/account';
import { BasicComponent } from '../basic.component';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})
export class AccountComponent extends BasicComponent implements OnInit {

  private countries : string[];
  private model: Account = new Account();

  constructor(
    alertService: AlertService,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService) {
      super(alertService)
    }

  ngOnInit(): void {
    this.accountService.getCountries().subscribe(countries => this.countries = countries);

    this.route.paramMap.subscribe(params => {
      let id = params.get('accountId');

      if(!id) {
        this.isNewDataSet = true;
        return;
      }

      this.loadAccount(id);
    });
  }

  loadAccount(accountId) {
    this.accountService.get(accountId).subscribe(account => {
      // assign user object
      this.model = account as Account;
      this.isNewDataSet = false;
    }, error => {
      // handle user not found
      this.isNewDataSet = true;
      this.handleError(error);
    });
  }

  save() {
    this.loading = true;

    if(this.isNewDataSet) {
      this.accountService.create(this.model)
        .subscribe(
          data => {
            this.alertService.success('Registration successful');
            this.router.navigate(['/login']);
          },
          err => this.handleError(err));
    } else {
      this.accountService.update(this.model)
        .subscribe(
          data => {
            this.alertService.success('Change successful');
          },
          err => this.handleError(err));
    }
  }
}
