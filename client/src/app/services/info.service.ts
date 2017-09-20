import { Injectable } from '@angular/core';
import { SecuredService } from './secured.service';
import { Http } from '@angular/http';
import { AppConfig } from '../app.config';
import { Observable } from 'rxjs/Observable';
import { ProjectInfo } from '../models/dtos/project.info';

@Injectable()
export class InfoService extends SecuredService {
  
    constructor(http: Http, config: AppConfig) {
      super(config.apiUrl + '/info', http);
    }

    getIds() : Observable<string[]> {
      return this._get('/ids');
    }

    getInfo(projectId : string) : Observable<ProjectInfo> {
      return this._get('/' + projectId);
    }
}
