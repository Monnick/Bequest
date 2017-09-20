import { Component, OnInit, Input } from '@angular/core';
import { ProjectData } from '../../../models/dtos/project.data';
import { AlertService } from '../../../services/alert.service';
import { ProjectService } from '../../../services/project.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BasicComponent } from '../../basic.component';
import { Country } from '../../../models/dtos/country';
import { Category } from '../../../models/dtos/category';
import { AccountService } from '../../../services/account.service';
import { Account } from '../../../models/dtos/account';
import { State } from '../../../models/state';
import { AppConfig } from '../../../app.config';

@Component({
  selector: 'project-edit-data',
  templateUrl: './project-edit-data.component.html'
})
export class ProjectEditDataComponent extends BasicComponent implements OnInit {

  private model : ProjectData = new ProjectData();
  private categories : Category[];
  private countries : Country[];
  private states : State[] = State.newState();
  private projectUrl : string;

  constructor(
    alertService : AlertService,
    private projectService : ProjectService,
    private accountService : AccountService,
    private route : ActivatedRoute,
    private router : Router,
    private config : AppConfig
  ) {
    super(alertService);
  }

  ngOnInit() {
    this.projectService.getCountries().subscribe(countries => this.countries = countries);
    this.projectService.getCategories().subscribe(categories => this.categories = categories);

    this.route.paramMap.subscribe(params => {
      let projectId = params.get('projectId');

      if(!projectId) {
        this.isNewDataSet = true;
        this.fillInitialData();
      }
      else
        this.loadProject(projectId);
    });
  }

  fillInitialData() {
    let currentAccount = JSON.parse(localStorage.getItem('currentAccount'));
    if (currentAccount && currentAccount.id) {
      this.accountService.get(currentAccount.id).subscribe(account => {
        this.buildProjectData(account);
      });
    }
  }

  buildProjectData(account : Account) {
    this.model.contactData.name = account.name;
    this.model.contactData.email = account.email;
    this.model.contactData.phone = account.phone;
    this.model.contactData.street = account.street;
    this.model.contactData.city = account.city;
    this.model.contactData.zip = account.zip;
    this.model.contactData.country = account.country;
  }

  loadProject(projectId) {
    this.projectService.getProjectData(projectId).subscribe(project => {
      this.model = project as ProjectData;
      this.projectUrl = this.config.apiUrl + '/overview/' + projectId;
      this.states = State.format(project.possibleStates);
      this.isNewDataSet = false;
    }, error => {
      this.isNewDataSet = true;
      this.handleError(error);
    });
  }
  
  save() {
    this.loading = true;

    if(this.isNewDataSet) {
      this.projectService.create(this.model)
        .subscribe(
          data => {
            this.alertService.success('Creation successful');
            this.router.navigate(['/project/' + data]);
          },
          err => this.handleError(err));
    } else {
      this.projectService.update(this.model)
        .subscribe(
          data => {
            this.alertService.success('Change successful');
          },
          err => this.handleError(err));
    }
  }
}
