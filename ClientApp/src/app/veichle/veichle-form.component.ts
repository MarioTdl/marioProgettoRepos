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
  makeId: number;
  modelId: number;
  isRegistred: Boolean;
  veichle: any = {
    features: [],
    contact: {
    }
  };



  constructor(private makeService: MakeService,
    private modelService: ModelService,
    private feauterService: FeaturesService
    ) { }

  ngOnInit() {
    this.makeService.getMakes().subscribe(res => {
      this.makes = res;
    });

    this.feauterService.getFeatures().subscribe(res => {
      this.features = res;
    });
  }

  onMakeChange() {
    this.models = this.modelsEmptyFake;
    this.modelService.getModelMakes(this.makeId).subscribe(res => {
      this.models = res;
    });
  }
  onFeatureToggle(feautureId, $event) {
    if ($event.target.checked) {
      this.veichle.features.push(feautureId);
    } else {
      const index = this.veichle.features.indexOf(feautureId);
      this.veichle.features.splice(index, 1);
    }
  }
  submit() {
    this.makeService.create(this.veichle)
      .subscribe(x => console.log(x),
        err => {

        });
  }
}

