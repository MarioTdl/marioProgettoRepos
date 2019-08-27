import { MakeService } from './../services/make.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-veichle-form',
  templateUrl: './veichle-form.component.html',
  styleUrls: ['./veichle-form.component.css']
})

export class VeichleFormComponent implements OnInit {
  makes;
  constructor(private makeService: MakeService) { }

  ngOnInit() {
    this.makeService.getMakes().subscribe(res => {
      this.makes = res;

    });
  };
}

