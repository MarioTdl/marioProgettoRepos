import { SaveVeichle } from './../model/SaveVeichle';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Veichle } from '../model/vehicle';

@Injectable({
  providedIn: 'root'
})

export class VeichleService {
  constructor(private http: HttpClient) { }
  private readonly veichleEndpoint = '/api/vehicles';
  create(veichle) {
    return this.http.post(this.veichleEndpoint, veichle);
  }
  getVeichle(id) {
    return this.http.get<any>(this.veichleEndpoint + '/' + id);
  }
  update(vehicle: SaveVeichle) {
    return this.http.put(this.veichleEndpoint + '/' + vehicle.id, vehicle);
  }
  delete(id) {
    return this.http.delete(this.veichleEndpoint + '/' + id);
  }
  getVeichles(filter) {
    return this.http.get<Veichle[]>(this.veichleEndpoint + '?' + this.toQueryString(filter));
  }
  toQueryString(obj) {
    const parts = [];
    for (let p in obj) {
      let value = obj[p];
      if (value != null && value != undefined) {
        parts.push(encodeURIComponent(p) + '=' + encodeURIComponent(value));
      }
    }
    return parts.join('&');
  }
}

