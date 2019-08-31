import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { BrowserXhr } from '@angular/http';

@Injectable()
export class ProgressService {
  private uploadProgress: Subject<any>;
  constructor() { }
  startTracking() {
    this.uploadProgress = new Subject();
    return this.uploadProgress;
  }
  notify(progres) {
    this.uploadProgress.next(progres);
  }
  endTracking() {
    this.uploadProgress.complete();
  }

}


@Injectable()
export class CustomBrowerXhrWithProgress extends BrowserXhr {

  constructor(private service: ProgressService) { super(); }

  build(): XMLHttpRequest {
    const xhr: XMLHttpRequest = super.build();

    xhr.upload.onprogress = (event) => {
      this.service.notify(this.createProgress(event));
    };

    xhr.upload.onloadend = () => {
      this.service.endTracking();
    };

    return xhr;
  }
  private createProgress(event) {
    return {
      total: event.total,
      percentage: Math.round(event.loaded / event.total * 100)
    };
  }
}
