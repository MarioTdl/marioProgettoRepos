import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { KeyValuePair } from '../model/KeyValuePair';


@Injectable({
  providedIn: 'root'
})
export class FeaturesService {

  constructor(private http: HttpClient) { }

  getFeatures() {
    return this.http.get<KeyValuePair[]>('/api/features');
  }
}
