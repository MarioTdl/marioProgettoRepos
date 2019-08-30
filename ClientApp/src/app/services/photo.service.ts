import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class PhotoService {

  constructor(private htpp: HttpClient) { }

  upload(veichleId, photo) {
    const formData = new FormData();
    formData.append('file', photo);
    return this.htpp.post('/api/vehicles/' + veichleId + '/photos', formData);
  }
}
