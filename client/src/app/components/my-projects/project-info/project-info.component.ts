import { Component, OnInit, Input } from '@angular/core';
import { BasicComponent } from '../../basic.component';
import { AlertService } from '../../../services/alert.service';
import { ProjectInfo } from '../../../models/dtos/project.info';
import { State } from '../../../models/state';
import { AppConfig } from '../../../app.config';

@Component({
  selector: 'project-info',
  templateUrl: './project-info.component.html'
})
export class ProjectInfoComponent extends BasicComponent implements OnInit {

  @Input('project') project : ProjectInfo;
  state : string = State.toText(0);
  url : string;
  states : State[];

  constructor(
    alertService : AlertService,
    private config :AppConfig,
  ) {
    super(alertService);
  }

  ngOnInit() {
    if(this.project) {
      this.url = this.config.apiUrl + '/pictures/' + this.project.id;
      this.state = State.toText(this.project.state);
      this.states = State.format(this.project.possibleStates);
    }
  }
}
