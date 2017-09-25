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
      this.categories = categories;

      if(categories.length > 1)
        this.categoryFilter = categories[0].title;
    }, error => {
      this.handleError(error);
    });
  }
}
