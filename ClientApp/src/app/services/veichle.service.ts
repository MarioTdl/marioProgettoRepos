import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Veichle } from '../model/vehicle';

@Injectable({
  providedIn: 'root'
})

export class VeichleService {
  constructor(private http: HttpClient) { }

  create(veichle) {
    return this.http.post('/api/vehicles', veichle);
  }
  getVeichle(id) {
    return this.http.get<any>('/api/vehicles/' + id);
  }
}
