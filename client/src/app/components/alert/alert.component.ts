import { Component, OnInit } from '@angular/core';
import { Message } from '../../models/message';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: 'alert',
  templateUrl: './alert.component.html'
})
export class AlertComponent {

  message : Message;
  
  constructor(private alertService : AlertService) {
    this.alertService.getMessage().subscribe(message => {
      this.message = message;
    });
  }
}
