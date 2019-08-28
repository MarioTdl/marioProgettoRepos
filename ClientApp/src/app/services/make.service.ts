import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Make } from '../model/makes';

@Injectable({
  providedIn: 'root'
})
export class MakeService {

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get<Make[]>('/api/makes');
  }
}
