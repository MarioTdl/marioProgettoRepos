import { Feature } from './../model/feature';
import { FeaturesService } from './../services/features.service';
import { ModelService } from './../services/model.service';
import { Model } from './../model/model';
import { MakeService } from './../services/make.service';
import { Component, OnInit } from '@angular/core';
import { Make } from '../model/makes';

@Component({
  selector: 'app-veichle-form',
  templateUrl: './veichle-form.component.html',
  styleUrls: ['./veichle-form.component.css']
})

export class VeichleFormComponent implements OnInit {
  makes: Make[];
  models: Model[];
  modelsEmptyFake: Model[];
  features: Feature[];
  modelsId: number;

  constructor(private makeService: MakeService,
    private modelService: ModelService,
    private feauterService: FeaturesService) { }

  ngOnInit() {
    this.makeService.getMakes().subscribe(res => {
      this.makes = res;
    });

    this.feauterService.getFeatures().subscribe(res => {
       this.features = res;
       console.log(res);
      });
  }

  onMakeChange() {
    this.models = this.modelsEmptyFake;
    this.modelService.getModelMakes(this.modelsId).subscribe(res => {
      this.models = res;
    });
  }
}

