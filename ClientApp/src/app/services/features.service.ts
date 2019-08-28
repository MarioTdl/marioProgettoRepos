import { Feature } from './../model/feature';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class FeaturesService {

  constructor(private http: HttpClient) { }

  getFeatures() {
    return this.http.get<Feature[]>('/api/features');
  }
}
