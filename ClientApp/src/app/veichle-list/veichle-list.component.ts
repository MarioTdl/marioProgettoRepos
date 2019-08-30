import { MakeService } from './../services/make.service';
import { KeyValuePair } from './../model/KeyValuePair';
import { Veichle } from './../model/Vehicle';
import { Component, OnInit } from '@angular/core';
import { VeichleService } from '../services/veichle.service';

@Component({
  selector: 'app-veichle-list',
  templateUrl: './veichle-list.component.html'
})
export class VeichleListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  queryResult: any = {};
  makes: KeyValuePair[];
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Id' },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    {}
  ];

  constructor(private veichleService: VeichleService, private makeService: MakeService) { }

  ngOnInit() {
    this.makeService.getMakes().subscribe(v => this.makes = v);
    this.popolateVehicles();
  }
  private popolateVehicles() {
    this.veichleService.getVeichles(this.query).subscribe(v => this.queryResult = v);
  }
  onFilterChange() {
    this.query.page = 1;
    this.popolateVehicles();
  }
  resetFilter() {
    this.query = { page: 1, pageSize: this.PAGE_SIZE };
    this.popolateVehicles();
  }
  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.popolateVehicles();
  }
  onPageChange(page) {
    this.query.page = page;
    this.popolateVehicles();
  }
}
