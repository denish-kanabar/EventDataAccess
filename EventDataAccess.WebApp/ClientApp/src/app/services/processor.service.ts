import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProcessorService {

  constructor(private http: HttpClient) { }

  //Method to call Processor API to start processing
  public startProcecessing(input: any): Observable<any> {
    let objProcessorResponse: Observable<any> = null;
    objProcessorResponse = this.http.post<any>(environment.PROCESSOR_ENDPOINT + 'startProcess', input);
    return objProcessorResponse;
  }

  //Method to call Processor API to retrive current state of DB for given input
  public startPolling(input: any): Observable<any> {
    let objResponse: Observable<any> = null;
    objResponse = this.http.get<any>(environment.PROCESSOR_ENDPOINT + 'getAggregator', { params: {'operationId':input} });
    return objResponse;
  }
}
