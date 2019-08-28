import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Model } from '../model/model';

@Injectable({
  providedIn: 'root'
})

export class ModelService {

  constructor(private http: HttpClient) { }

  getModelMakes(id: number) {
    if (id > 0) {
    return this.http.get<Model[]>('/api/model/' + id);
    }
  }
}
