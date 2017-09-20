import { Component, OnInit } from '@angular/core';
import { State } from '../../models/state';
import { AlertService } from '../../services/alert.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html'
})
export class ProjectComponent implements OnInit {

  private newProject : boolean = true;
  
  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      let id = params.get('projectId');

      if(id)
        this.newProject = false;
    });
  }
}
