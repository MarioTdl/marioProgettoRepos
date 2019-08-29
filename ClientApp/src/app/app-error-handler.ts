import { ErrorHandler } from '@angular/core';

export class AppErroHandler implements ErrorHandler {
  handleError(error: any): void {
    console.log('ERROR');
  }

}
