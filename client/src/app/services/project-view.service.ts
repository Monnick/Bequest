import { Country } from './../models/dtos/country';
import { Category } from './../models/dtos/category';
import { ProjectThumbnail } from './../models/dtos/project.thumbnail';
import { SecuredService } from './secured.service';
import { ProjectView } from './../models/dtos/project.view';
import { Observable } from 'rxjs/Observable';
import { Http } from '@angular/http';
import { AppConfig } from './../app.config';
import { Injectable } from '@angular/core';
import { URLSearchParams } from '@angular/http';

@Injectable()
export class ProjectViewService extends SecuredService {

  constructor(http: Http, config: AppConfig) {
    super(config.apiUrl + '/overview', http);
  }

  get(projectId : string) : Observable<ProjectView> {
    return this._get('/' + projectId);
  }
  
  getCountries() : Observable<Country[]> {
    return this._get('/countries');
  }
  
  getCategories() : Observable<Category[]> {
    return this._get('/categories');
  }

  getMany(category : string, country : string, orderBy : string, page : string, pageSize : string) : Observable<ProjectThumbnail[]> {
    let params: URLSearchParams = new URLSearchParams();

    if(category)
      params.set('category', category);

    if(country)
      params.set('country', country);

    if(orderBy)
      params.set('orderBy', orderBy);

    if(page)
      params.set('page', page);

    if(pageSize)
      params.set('size', pageSize);

    return this._get('/', params);
  }
}
