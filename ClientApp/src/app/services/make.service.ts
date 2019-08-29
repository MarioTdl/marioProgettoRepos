import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { KeyValuePair } from '../model/KeyValuePair';

@Injectable({
  providedIn: 'root'
})
export class MakeService {

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get<KeyValuePair[]>('/api/makes');
  }
}
