import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../services/alert.service';
import { InfoService } from '../../services/info.service';
import { BasicComponent } from '../basic.component';
import { ProjectInfo } from '../../models/dtos/project.info';

@Component({
  selector: 'app-my-projects',
  templateUrl: './my-projects.component.html'
})
export class MyProjectsComponent extends BasicComponent implements OnInit {

  private projects : ProjectInfo[];

  constructor(
    alertService : AlertService,
    private projectService : InfoService
  ) {
    super(alertService);
  }

  ngOnInit() {
    this.projectService.getProjects().subscribe(projects => {
      // assign user object
      this.projects = projects;
      this.isNewDataSet = false;
    }, error => {
      // handle user not found
      this.isNewDataSet = true;
      this.handleError(error);
    });
  }

}
