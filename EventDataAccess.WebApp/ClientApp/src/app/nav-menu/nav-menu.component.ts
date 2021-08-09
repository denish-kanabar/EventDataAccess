import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isProcessStarted = false;

  constructor(private dataService: DataService) { }

  //Reads current state of processing to enable or disaply navigation menus options
  ngOnInit() {
    this.dataService.currentIsProcessStarted.subscribe((res) => {
      this.isProcessStarted = res;
    });

  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
