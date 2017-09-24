import { ProjectView } from './../../models/dtos/project.view';
import { ProjectViewService } from './../../services/project-view.service';
import { AppConfig } from './../../app.config';
import { BasicComponent } from './../basic.component';
import { AlertService } from './../../services/alert.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-project-view',
  templateUrl: './project-view.component.html'
})
export class ProjectViewComponent extends BasicComponent implements OnInit {

  projectId : string;
  pictureUrl : string;
  model : ProjectView;
  
  constructor(
    alertService : AlertService,
    private config : AppConfig,
    private route : ActivatedRoute,
    private viewService : ProjectViewService
  ) {
    super(alertService);
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.projectId = params.get('projectId');

      if(this.projectId) {
        this.loadProject(this.projectId);
      }
    });
  }
  
  loadProject(projectId) {
    this.viewService.get(projectId).subscribe(view => {
      this.model = view;
      this.pictureUrl = this.config.apiUrl + '/pictures/' + projectId;
    }, error => {
      this.handleError(error);
    });
  }
}
