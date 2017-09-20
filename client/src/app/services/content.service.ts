import { Injectable } from '@angular/core';
import { SecuredService } from './secured.service';
import { Http } from '@angular/http';
import { AppConfig } from '../app.config';
import { Observable } from 'rxjs/Observable';
import { Content } from '../models/dtos/content';

@Injectable()
export class ContentService extends SecuredService {

  constructor(http: Http, config: AppConfig) {
    super(config.apiUrl + '/content', http);
  }

  get(projectId : string) : Observable<Content> {
    return this._get('/' + projectId);
  }

  update(content : Content) {
    return this._update(content);
  }
}
