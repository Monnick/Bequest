import { InfoService } from './../../../services/info.service';
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
  valueChanged : boolean;

  constructor(
    alertService : AlertService,
    private config : AppConfig,
    private infoService : InfoService
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

  anyValueChanged() {
    this.valueChanged = true;
  }

  save() {
    this.valueChanged = false;
    
    this.infoService.updateItems(this.project.id, this.project.neededItems)
      .subscribe(
        data => {
          this.alertService.success('Change successful');
          this.loading = false;
        },
        err => this.handleError(err));
  }
}
