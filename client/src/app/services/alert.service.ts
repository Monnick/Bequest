import { Injectable } from '@angular/core';
import { Message } from "../models/message";
import { Subject } from "rxjs/Subject";
import { Observable } from "rxjs/Observable";
import { Router, NavigationStart } from "@angular/router";

@Injectable()
export class AlertService {

  private subject = new Subject<Message>();

  constructor(private router : Router) {
    // clear alert message on route change
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        this.subject.next();
      }
  });
  }
 
  clearMessage() {
    this.subject.next();
  }

  success(message: string) {
    this.subject.next(new Message('success', message));
  }

  error(message: string) {
    this.subject.next(new Message('error', message));
  }

  getMessage(): Observable<Message> {
    return this.subject.asObservable();
  }
}
