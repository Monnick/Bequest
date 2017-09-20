import { DataService } from "./data.service";
import { AppConfig } from "../app.config";
import { Http, Headers, Response, RequestOptions } from "@angular/http";


export class SecuredService extends DataService {
    
  constructor(url : string, http : Http) {
    super(url, http);
  }

  // protected jwt() : RequestOptions {
  //   // create authorization header with jwt token
  //   let currentAccount = JSON.parse(localStorage.getItem('currentAccount'));
  //   if (currentAccount && currentAccount.token) {
  //     let headers = new Headers({ 'Authorization': 'Bearer ' + currentAccount.token });
  //     return new RequestOptions({ headers: headers });
  //   }
  // }

  protected jwt(headervalues? : any[]) : RequestOptions {
    // create authorization header with jwt token
    let currentAccount = JSON.parse(localStorage.getItem('currentAccount'));
    if (currentAccount && currentAccount.token) {
      let headers = new Headers({ 'Authorization': 'Bearer ' + currentAccount.token });
      // if(headervalues) {
      //   for (var i = 0; i < headervalues.length; i++) {
      //     headers.append(headervalues[i].name, headervalues[i].value);
      //   }
      // }
      return new RequestOptions({ headers: headers });
    }
  }

  protected _get(path : string, headervalues? : any[]) {
    return this.http.get(this.url + path, this.jwt(headervalues))
      .map((response: Response) => response.json())
      .catch(this.handleError);
  }

  protected _create(path : string, ressouce: any, headervalues? : any[]) {
    return this.http.post(this.url + path, ressouce, this.jwt(headervalues))
      .map((response: Response) => response.json())
      .catch(this.handleError);
  }

  protected _update(ressource : any, headervalues? : any[]) {
    return this.http.put(this.url, ressource, this.jwt(headervalues))
    .map((response: Response) => response.json())
    .catch(this.handleError);
  }
}