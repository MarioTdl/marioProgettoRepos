import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MakeService {
makes;
  constructor(private http:HttpClient) { }

  getMakes() {
    return this.http.get<any>('/api/makes');
    };
}
