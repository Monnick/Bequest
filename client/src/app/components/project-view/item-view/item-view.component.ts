import { NeededItem } from './../../../models/dtos/neededItem';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'item-view',
  templateUrl: './item-view.component.html'
})
export class ItemViewComponent {

  @Input('item') item : NeededItem;
}
