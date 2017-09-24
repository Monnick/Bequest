import { NeededItems } from './../models/dtos/neededItems';
import { NeededItem } from './../models/dtos/neededItem';
import { State } from './../models/state';
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

  getProjects() : Observable<ProjectInfo[]> {
    return this._get('/');
  }

  updateItems(id : string, neededItems : NeededItem[]) {
    let i = new NeededItems();
    i.projectId = id;
    i.items = neededItems;

    return this._update(i);
  }
}
