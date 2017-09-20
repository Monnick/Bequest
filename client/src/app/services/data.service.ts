import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { BadInput } from "../errors/bad-input";
import { NotFoundError } from "../errors/not-found-error";
import { AppError } from "../errors/app-error";
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { Unauthorised } from "../errors/unauthorised";
import { BadGateway } from "../errors/bad-gateway";
import { ServerError } from '../errors/server-error';

export class DataService {

  constructor(protected url: string, protected http: Http) { }

  //create(resource) {
  //  return this.http.post(this.url, JSON.stringify(resource))
  //    .map(response => response.json())
  //    .catch(this.handleError);
  //}

  protected handleError(error: Response) {
    if (error.status === 400)
      return Observable.throw(new BadInput(error));
  
    if (error.status === 404)
      return Observable.throw(new NotFoundError());
    
    if(error.status == 401)
      return Observable.throw(new Unauthorised(error));
    
    if(error.status == 500)
      return Observable.throw(new ServerError(error));

    if(error.status == 502)
      return Observable.throw(new BadGateway(error));

    return Observable.throw(new AppError(error));
  }
}
