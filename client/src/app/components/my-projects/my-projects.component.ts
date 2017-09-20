import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../services/alert.service';
import { InfoService } from '../../services/info.service';
import { BasicComponent } from '../basic.component';

@Component({
  selector: 'app-my-projects',
  templateUrl: './my-projects.component.html'
})
export class MyProjectsComponent extends BasicComponent implements OnInit {

  private ids : string[];

  constructor(
    alertService : AlertService,
    private projectService : InfoService
  ) {
    super(alertService);
  }

  ngOnInit() {
    this.projectService.getIds().subscribe(ids => {
      // assign user object
      this.ids = ids;
      this.isNewDataSet = false;
    }, error => {
      // handle user not found
      this.isNewDataSet = true;
      this.handleError(error);
    });
  }

}
