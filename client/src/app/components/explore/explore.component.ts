import { ActivatedRoute } from '@angular/router';
import { Country } from './../../models/dtos/country';
import { Category } from './../../models/dtos/category';
import { ProjectViewService } from './../../services/project-view.service';
import { AlertService } from './../../services/alert.service';
import { BasicComponent } from './../basic.component';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-explore',
  templateUrl: './explore.component.html'
})
export class ExploreComponent extends BasicComponent implements OnInit {

  currentFilter : string;
  categories : string[];
  activeCategory : string;
  countries : string[];
  activeCountry : string;

  constructor(
    alertService : AlertService,
    private route : ActivatedRoute,
    private viewService : ProjectViewService
  ) {
    super(alertService);
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      let filter = params.get('filter');
      let filterPreset = params.get('preset');

      if(filter != this.currentFilter) {
        this.categories = undefined;
        this.activeCategory = undefined;
        this.countries = undefined;
        this.activeCountry = undefined;
        this.currentFilter = filter;
      }

      if(filter === 'country')
        this.loadCountries(filterPreset);
      else if (filter === 'category')
        this.loadCategories(filterPreset);
    });
  }

  loadCategories(preset : string) {
    this.viewService.getCategories().subscribe(categories => {
      this.categories = categories.map(category => category.title);

      if(!preset) {
        if(categories.length > 1)
          this.activeCategory = categories[0].title;
      }
      else
        this.activeCategory = preset;
    }, error => {
      this.handleError(error);
    });
  }

  loadCountries(preset : string) {
    this.viewService.getCountries().subscribe(countries => {
      this.countries = countries.map(country => country.name);

      if(!preset) {
        if(countries.length > 1)
          this.activeCountry = countries[0].name;
      }
      else
        this.activeCountry = preset;
    }, error => {
      this.handleError(error);
    });
  }
}
