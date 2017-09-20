import { Injectable } from '@angular/core';
import { SecuredService } from './secured.service';
import { Http } from '@angular/http';
import { AppConfig } from '../app.config';
import { NeededItems } from '../models/dtos/neededItems';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ItemService extends SecuredService {
  
    constructor(http: Http, config: AppConfig) {
      super(config.apiUrl + '/items', http);
    }
  
    get(projectId : string) : Observable<NeededItems> {
      return this._get('/' + projectId);
    }
  
    update(items : NeededItems) {
      return this._update(items);
    }
}
