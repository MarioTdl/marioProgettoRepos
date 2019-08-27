import { ModelService } from './../services/model.service';
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
  modelsEmptyFake: Model[];
  modelsId: number;

  constructor(private makeService: MakeService, private modelService: ModelService) { }

  ngOnInit() {
    this.makeService.getMakes().subscribe(res => {
      this.makes = res;
    });
  }

  onMakeChange() {
    this.models = this.modelsEmptyFake;
    this.modelService.getModelMakes(this.modelsId).subscribe(res => {
      this.models = res;
    });
  }
}

