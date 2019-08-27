import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MakeService {

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get<any>('/api/makes');
  }

  getModelMakes(id: number) {
    return this.http.get<any>('/api/model/' + id);
  }
}
