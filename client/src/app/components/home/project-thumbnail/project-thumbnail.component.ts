import { ProjectThumbnail } from './../../../models/dtos/project.thumbnail';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'project-thumbnail',
  templateUrl: './project-thumbnail.component.html'
})
export class ProjectThumbnailComponent {

  @Input('project') project : ProjectThumbnail;
}
