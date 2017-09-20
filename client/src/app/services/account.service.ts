import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
 
import { AppConfig } from '../app.config';
import { Account } from "../models/dtos/account";
import { SecuredService } from './secured.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AccountService extends SecuredService {

  constructor(http: Http, config: AppConfig) {
      super(config.apiUrl + '/accounts', http);
  }
  
  get(id : string) : Observable<Account> {
    return this._get('/' + id);
  }

  create(account: Account) {
    return this._create('/register', account);
  }

  update(account: Account) {
    return this._update(account);
  }
  
  getCountries() {
    return this._get('/countries');
  }
}
