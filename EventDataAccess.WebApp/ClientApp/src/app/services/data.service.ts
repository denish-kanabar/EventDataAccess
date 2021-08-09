import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  private isProcessStarted = new BehaviorSubject(false);

  //Reads current state of process task
  currentIsProcessStarted = this.isProcessStarted.asObservable();

  //updates current state of Processing to true or false
  changeIsProcessStarted(IsProcessStarted: boolean) {
    this.isProcessStarted.next(IsProcessStarted);
  }

}
