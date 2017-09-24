import { BasicComponent } from './../../basic.component';
import { AlertService } from './../../../services/alert.service';
import { SecuredService } from './../../../services/secured.service';
import { ProjectViewService } from './../../../services/project-view.service';
import { ProjectThumbnail } from './../../../models/dtos/project.thumbnail';
import { Component, Input, OnInit, OnChanges } from '@angular/core';

@Component({
  selector: 'paged-view',
  templateUrl: './paged-view.component.html'
})
export class PagedViewComponent extends BasicComponent implements OnInit, OnChanges {

  readonly PAGE_SIZE : number = 3;
  projects : ProjectThumbnail[];
  @Input('delayedLoading') delayedLoading : boolean;
  @Input('title') title : string;
  @Input('orderBy') orderBy : string;
  @Input('page') page : number = 0;
  @Input('category') category : string;
  @Input('country') country : string;

  constructor(
    alertService : AlertService,
    private viewService : ProjectViewService
  ) {
    super(alertService);
  }

  ngOnInit() {
    if(!this.delayedLoading)
      this.loadProjects();
  }

  ngOnChanges() {
    if(this.delayedLoading)
      this.loadProjects();
  }

  loadProjects() {
    this.viewService.getMany(this.category, this.country, this.orderBy, this.page.toString(), this.PAGE_SIZE.toString()).subscribe(projects => {
      this.projects = projects;
    }, error => {
      this.handleError(error);
    });
  }

  onPrevious() {
    if(this.page === 0)
      return;

    this.page--;
    this.loadProjects();
  }

  onNext() {
    if(this.projects.length < this.PAGE_SIZE)
      return;

    this.page++;
    this.loadProjects();
  }

  setCategory(category : string) {
    this.category = category;
    this.page = 0;
    this.loadProjects();
  }
  
    setCountry(country : string) {
      this.country = country;
      this.page = 0;
      this.loadProjects();
    }
}
