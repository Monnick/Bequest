import { AlertService } from './../../services/alert.service';
import { BasicComponent } from './../basic.component';
import { Category } from './../../models/dtos/category';
import { Country } from './../../models/dtos/country';
import { ProjectViewService } from './../../services/project-view.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent extends BasicComponent implements OnInit {

  countries : Country[];
  categories : Category[];
  categoryFilter : string;

  constructor(
    alertService : AlertService,
    private viewService : ProjectViewService
  ) {
    super(alertService);
  }

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.viewService.getCategories().subscribe(categories => {
      this.categories = categories
    }, error => {
      this.handleError(error);
    });
  }

  loadCountries() {
    this.viewService.getCountries().subscribe(countries => {
      this.countries = countries
    }, error => {
      this.handleError(error);
    });
  }
}
