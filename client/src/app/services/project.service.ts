import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { AppConfig } from '../app.config';
import { SecuredService } from './secured.service';
import { ProjectData } from '../models/dtos/project.data';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ProjectService extends SecuredService {

  constructor(http: Http, config: AppConfig) {
      super(config.apiUrl + '/project', http);
  }

  getProjectData(projectId) : Observable<ProjectData> {
    return this._get('/' + projectId);
  }
  
  create(project: ProjectData) {
    return this._create('/', project);
  }

  update(project: ProjectData) {
    return this._update(project);
  }
  
  getCountries() {
    return this._get('/countries');
  }
  
  getCategories() {
    return this._get('/categories');
  }
}
