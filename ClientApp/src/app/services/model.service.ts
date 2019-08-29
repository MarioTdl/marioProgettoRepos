import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { KeyValuePair } from '../model/KeyValuePair';

@Injectable({
  providedIn: 'root'
})

export class ModelService {

  constructor(private http: HttpClient) { }

  getModelMakes(id: number) {
    if (id > 0) {
    return this.http.get<KeyValuePair[]>('/api/model/' + id);
    }
  }
}
