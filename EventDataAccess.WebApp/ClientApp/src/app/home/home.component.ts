import { Component, OnInit } from '@angular/core';
import { ProcessorService } from '../services/processor.service';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators'
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  batchInput: any = { numberCount: 1, batchCount: 1, operationId: 0 };
  batchList: any = [];
  countDown: Subscription;
  isProcessComplete: boolean = false;
  batchTotal: any = [];
  operationId: number = 0;

  constructor(private processorService: ProcessorService, private dataService: DataService) { }

  ngOnInit() {
    this.operationId = Number(sessionStorage.getItem("previousOperationId")) == undefined ? 0 : Number(sessionStorage.getItem("previousOperationId"));
    this.processorService.startPolling(this.operationId).subscribe((res) => {
      this.batchList = res;
    });
  }

  //Submits requet for processing
  submit() {
    this.isProcessComplete = true;
    this.dataService.changeIsProcessStarted(true);
    this.batchInput.operationId = this.operationId + 1;
    this.batchList = [];

    this.processorService.startProcecessing(this.batchInput).subscribe((res) => {
    });

    this.countDown = timer(7000, 2000).pipe(switchMap(() => {
      return this.processorService.startPolling(this.batchInput.operationId);
    })).subscribe((res) => {
      this.batchList = res;
      var flattened = [].concat.apply([], this.batchList);
      var listStatus = typeof flattened.find(x => x.multipliedNum == 0);
      if (listStatus == 'undefined') {
        this.countDown.unsubscribe();
        this.dataService.changeIsProcessStarted(false);
        sessionStorage.setItem("previousOperationId", this.batchInput.operationId.toString());
        this.isProcessComplete = false;
      }
    });
  }

  //Calculates Total of All batches multiplied numbers
  getTotal() {
    var flattened = [].concat.apply([], this.batchList);
    return flattened.reduce((acc, val) => acc += val.multipliedNum, 0).toString();
  }

  //Calculates Total of All multiplied numbers of a batch
  getBatchTotal(input: any) {
    return input.reduce((acc, val) => acc += val.multipliedNum, 0).toString();
  }

  ngOnDestroy() {
    this.countDown = null;
  }
}

