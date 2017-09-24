import { DataService } from "./data.service";
import { AppConfig } from "../app.config";
import { Http, Headers, Response, RequestOptions, URLSearchParams } from "@angular/http";


export class SecuredService extends DataService {
    
  constructor(url : string, http : Http) {
    super(url, http);
  }

  protected jwt(searchParams? : URLSearchParams) : RequestOptions {
    // create authorization header with jwt token
    let currentAccount = JSON.parse(localStorage.getItem('currentAccount'));
    if (currentAccount && currentAccount.token) {
      let headers = new Headers({ 'Authorization': 'Bearer ' + currentAccount.token });
      return new RequestOptions({ headers: headers, params: searchParams });
    }
    return new RequestOptions({ params: searchParams });
  }

  protected _get(path : string, searchParams? : URLSearchParams) {
    return this.http.get(this.url + path, this.jwt(searchParams))
      .map((response: Response) => response.json())
      .catch(this.handleError);
  }

  protected _create(path : string, ressouce: any) {
    return this.http.post(this.url + path, ressouce, this.jwt())
      .map((response: Response) => response.json())
      .catch(this.handleError);
  }

  protected _update(ressource : any) {
    return this.http.put(this.url, ressource, this.jwt())
    .map((response: Response) => response.json())
    .catch(this.handleError);
  }
}