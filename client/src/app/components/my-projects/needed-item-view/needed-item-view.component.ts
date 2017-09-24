import { NeededItem } from './../../../models/dtos/neededItem';
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'needed-item-view',
  templateUrl: './needed-item-view.component.html',
  styleUrls: ['./../../../../assets/stylesheets/centeredTable.css']
})
export class NeededItemViewComponent {

  @Input('items') items : NeededItem[];
  @Output('valueChange') valueChange : EventEmitter<boolean>;
  
  constructor() {
    this.valueChange = new EventEmitter();
  }

  updateValue() {
    this.valueChange.emit(true);
  }
}
