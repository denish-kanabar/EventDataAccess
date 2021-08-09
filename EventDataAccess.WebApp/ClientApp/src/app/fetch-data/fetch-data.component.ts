import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProcessorService } from '../services/processor.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {

  batchList: any = [];

  constructor(private processorService: ProcessorService) { }

  //Retrives information of batches execute prior to current processing
  ngOnInit() {
    let operationId = Number(sessionStorage.getItem("previousOperationId")) == undefined ? 0 : Number(sessionStorage.getItem("previousOperationId"))-1;
    this.processorService.startPolling(operationId).subscribe((res) => {
      this.batchList = res == null ? []: res;
    });
  }

  getBatchTotal(input: any) {
    return input.reduce((acc, val) => acc += val.multipliedNum, 0).toString();
  }

  getTotal() {
    var flattened = [].concat.apply([], this.batchList);
    return flattened.reduce((acc, val) => acc += val.multipliedNum, 0).toString();
  }

}
