import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BasicComponent } from '../../basic.component';
import { NeededItems } from '../../../models/dtos/neededItems';
import { AlertService } from '../../../services/alert.service';
import { ItemService } from '../../../services/item.service';
import { ActivatedRoute } from '@angular/router';
import { NeededItem } from '../../../models/dtos/neededItem';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';

@Component({
  selector: 'project-edit-items',
  templateUrl: './project-edit-items.component.html',
  styleUrls: ['./../../../../assets/stylesheets/centeredTable.css']
})
export class ProjectEditItemsComponent extends BasicComponent implements OnInit {

  private projectId : string;
  private items : NeededItems = new NeededItems();

  constructor(
    alertService : AlertService,
    private itemService : ItemService,
    private route : ActivatedRoute
  ) {
    super(alertService);
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.items.projectId = params.get('projectId');

      if(this.items.projectId)
        this.loadProject(this.items.projectId);
    });
  }

  loadProject(projectId) {
    this.itemService.get(projectId).subscribe(items => {
      // assign user object
      this.items = items;
      this.isNewDataSet = false;
    }, error => {
      // handle user not found
      this.isNewDataSet = true;
      this.handleError(error);
    });
  }

  add() {
    let newItem = new NeededItem();
    this.items.items.push(newItem);
  }

  removeItem(item) {
    let index = this.items.items.indexOf(item);
    this.items.items.splice(index, 1);
  }

  saveItems() {
    this.loading = true;

    this.itemService.update(this.items)
      .subscribe(
        data => {
          this.alertService.success('Change successful');
          this.loading = false;
        },
        err => this.handleError(err));
  }
}
