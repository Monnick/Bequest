import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { AppConfig } from "../app.config";
import { Subject } from "rxjs/Subject";
import { DataService } from "./data.service";

@Injectable()
export class AuthenticationService extends DataService {

  private subject = new Subject<any>();

  constructor(http: Http, config: AppConfig) {
    super(config.apiUrl + '/accounts', http);
  }

  login(login: string, password: string) {
    return this.http.post(this.url + '/authenticate', { login: login, password: password })
    .map((response: Response) => {
      // login successful if there's a jwt token in the response
      let account = response.json();
      if (account && account.token) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentAccount', JSON.stringify(account));
        this.subject.next({ id : account.id, loggedIn : true });
      }
    }).catch(this.handleError);
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentAccount');
    this.subject.next({ loggedIn : false });
  }

  status() : Observable<any> {
    return this.subject.asObservable();
  }
}
