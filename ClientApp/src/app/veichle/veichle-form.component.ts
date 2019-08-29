import * as _ from 'lodash';
import { Veichle } from './../model/Vehicle';
import { SaveVeichle } from './../model/SaveVeichle';
import { FeaturesService } from './../services/features.service';
import { ModelService } from './../services/model.service';
import { MakeService } from './../services/make.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { VeichleService } from '../services/veichle.service';

@Component({
  selector: 'app-veichle-form',
  templateUrl: './veichle-form.component.html',
  styleUrls: ['./veichle-form.component.css']
})

export class VeichleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  modelsEmptyFake: any[];
  features: any[];
  veichle: SaveVeichle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistred: false,
    features: [],
    contact: {
      name: '',
      phone: '',
      email: ''
    }
  };



  constructor(private makeService: MakeService,
    private route: ActivatedRoute,
    private router: Router,
    private modelService: ModelService,
    private feauterService: FeaturesService,
    private veichleService: VeichleService
  ) {
    route.params.subscribe(p => {
      this.veichle.id = +p['id'];
    });
  }

  ngOnInit() {
    // tslint:disable-next-line: prefer-const
    let source = [
      this.makeService.getMakes(),
      this.feauterService.getFeatures()
    ];
    if (this.veichle.id) {
      source.push(this.veichleService.getVeichle(this.veichle.id));
    }

    forkJoin(source).subscribe(data => {
      this.makes = data[0];
      this.features = data[1];
      if (this.veichle.id) {
        this.setVeichle(data[2] as any);
      }
    },
      err => {
        if (err.status === 404) {
          this.router.navigate(['']);
        }
      });
  }

  private setVeichle(v: Veichle) {
    console.log(v);
    this.veichle.id = v.id;
    this.veichle.makeId = v.make.id;
    this.veichle.modelId = v.model.id;
    this.veichle.isRegistred = v.isRegistred;
    this.veichle.contact = v.contact;
    this.veichle.features = _.pluck(v.features, 'id');
  }

  onMakeChange() {
    this.models = this.modelsEmptyFake;
    this.modelService.getModelMakes(this.veichle.makeId).subscribe(res => {
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
    if (this.veichle.id) {
      this.veichleService.update(this.veichle).subscribe(x => console.log(x));
    }
    this.veichleService.create(this.veichle)
      .subscribe(x => console.log(x));
  }
  delete() {
    if (confirm('Sei sicuro?')) {
      this.veichleService.delete(this.veichle.id).subscribe(x => { this.router.navigate(['']); });
    }
  }
}

