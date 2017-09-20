import { Component, OnInit, Input } from '@angular/core';
import { InfoService } from '../../../services/info.service';
import { BasicComponent } from '../../basic.component';
import { AlertService } from '../../../services/alert.service';
import { ProjectInfo } from '../../../models/dtos/project.info';
import { State } from '../../../models/state';
import { AppConfig } from '../../../app.config';

@Component({
  selector: 'project-info',
  templateUrl: './project-info.component.html'
})
export class ProjectInfoComponent  extends BasicComponent implements OnInit {

  @Input('projectId') projectId : string;
  project : ProjectInfo = new ProjectInfo();
  state : string = State.toText(0);
  url : string;
  states : State[];

  constructor(
    alertService : AlertService,
    private config :AppConfig,
    private service : InfoService
  ) {
    super(alertService);
  }

  ngOnInit() {
    if(this.projectId) {
      this.service.getInfo(this.projectId).subscribe(project => {
        this.project = project;
        this.url = this.config.apiUrl + '/pictures/' + this.projectId;
        this.state = State.toText(project.state);
        this.states = State.format(project.possibleStates);
      }, error => {
        this.handleError(error);
      });
    }
  }
}
