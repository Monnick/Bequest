import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'up-down-input',
  templateUrl: './up-down-input.component.html'
})
export class UpDownInputComponent {

  @Input('additionalClasses') additionalClasses: string;
  @Input('value') value : number = 0;
  @Input('min') min : number = 0;
  @Output('valueChange') valueChange : EventEmitter<number>;

  constructor() {
      this.valueChange = new EventEmitter();
  }

  increase() {
    if(!this.value)
      this.value = 0;
    
    this.value++;
    this.updateValue();
  }

  decrease() {
    if(!this.value)
      this.value = 0;
  
    if(this.value == this.min)
      return;

    this.value--;
    this.updateValue();
  }

  updateValue() {
    this.valueChange.emit(this.value);
  }
}
