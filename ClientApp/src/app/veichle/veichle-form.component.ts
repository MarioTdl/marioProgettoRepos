import { Model } from './../model/model';
import { Makes } from './../model/makes';
import { MakeService } from './../services/make.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-veichle-form',
  templateUrl: './veichle-form.component.html',
  styleUrls: ['./veichle-form.component.css']
})

export class VeichleFormComponent implements OnInit {
  makes: Makes[];
  models: Model[];
  modelsId:number;

  constructor(private makeService: MakeService) { }

  ngOnInit() {
    this.makeService.getMakes().subscribe(res => {
      this.makes = res;
    });
  }

  onMakeChange() {
    this.makeService.getModelMakes(this.modelsId).subscribe(res => {
      this.models = res;
    });

  }
}

