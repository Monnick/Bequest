import { Component, OnInit, Input, Output, EventEmitter, AfterViewInit, OnDestroy } from '@angular/core';
import { BasicComponent } from '../../basic.component';
import { AlertService } from '../../../services/alert.service';
import { ContentService } from '../../../services/content.service';
import { ActivatedRoute } from '@angular/router';
import { Content } from '../../../models/dtos/content';

@Component({
  selector: 'project-edit-content',
  templateUrl: './project-edit-content.component.html'
})
export class ProjectEditContentComponent extends BasicComponent implements OnInit {

  private content : Content = new Content();

  constructor(
    alertService : AlertService,
    private route : ActivatedRoute,
    private contentService : ContentService
  ) {
    super(alertService);
  }

  doNothing() {
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.content.projectId = params.get('projectId');

      if(this.content.projectId)
        this.loadProject(this.content.projectId);
    });
  }

  loadProject(projectId) {
    this.contentService.get(projectId).subscribe(content => {
      // assign user object
      this.content.data = content.data;
      this.isNewDataSet = false;
    }, error => {
      // handle user not found
      this.isNewDataSet = true;
      this.handleError(error);
    });
  }

  save() {
    this.loading = true;

    this.contentService.update(this.content)
    .subscribe(
      data => {
        this.alertService.success('Change successful');
        this.loading = false;
      },
      err => this.handleError(err));
  }
}
