import { Injectable } from '@angular/core';
import { SecuredService } from './secured.service';
import { AppConfig } from '../app.config';
import { Http } from '@angular/http';
import { Picture } from '../models/dtos/picture';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class PictureService extends SecuredService {
  
    constructor(http: Http, config: AppConfig) {
      super(config.apiUrl + '/pictures', http);
    }
  
    get(projectId : string) : Observable<Picture> {
      return this._get('/' + projectId);
    }
  
    upload(picture : Picture) {
      return this._update(picture);
    }
}
