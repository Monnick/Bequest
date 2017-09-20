import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'zippy',
  templateUrl: './zippy.component.html'
})
export class ZippyComponent {

  @Input('isEnabled') isEnabled : boolean = true;
  @Input('title') title : string;
  @Input('isExpanded') isExpanded : boolean = false;

  onClicked() {
    if(this.isEnabled)
      this.isExpanded = !this.isExpanded;
  }
}
