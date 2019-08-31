import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Photo } from '../model/Photo';

@Injectable()
export class PhotoService {

  constructor(private htpp: HttpClient) { }

  upload(veichleId, photo) {
    const formData = new FormData();
    formData.append('file', photo);
    return this.htpp.post<Photo>('/api/vehicles/' + veichleId + '/photos', formData);
  }
  getPhotos(veichleId) {
    return this.htpp.get<Photo[]>('/api/vehicles/' + veichleId + '/photos');
  }
  
}
