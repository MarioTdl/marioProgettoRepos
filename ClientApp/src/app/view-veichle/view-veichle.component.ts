import { ProgressService } from './../services/progress.service';
import { VeichleService } from './../services/veichle.service';
import { Component, OnInit, ElementRef, ViewChild, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PhotoService } from '../services/photo.service';
import { Photo } from '../model/Photo';

@Component({
  selector: 'app-view-veichle',
  templateUrl: './view-veichle.component.html'
})
export class ViewVeichleComponent implements OnInit {
  vehicle: any;
  vehicleId: number;
  @ViewChild('fileInput') fileInput: ElementRef;
  photos: Photo[];
  progress: any;

  constructor(
    private zone: NgZone,
    private route: ActivatedRoute,
    private router: Router,
    private photoService: PhotoService,
    private vehicleService: VeichleService,
    private progressService: ProgressService) {

    route.params.subscribe(p => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(['/allVeichles']);
        return;
      }
    });
  }

  ngOnInit() {
    this.photoService.getPhotos(this.vehicleId)
      .subscribe(photos => this.photos = photos);

    this.vehicleService.getVeichle(this.vehicleId)
      .subscribe(
        v => this.vehicle = v,
        err => {
          if (err.status === 404) {
            this.router.navigate(['/allVeichles']);
            return;
          }
        });
  }

  delete() {
    if (confirm('Are you sure?')) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/allVeichles']);
        });
    }
  }
  uploadPhoto() {
    const nativeHelement: HTMLInputElement = this.fileInput.nativeElement;

    this.progressService.startTracking().subscribe(progress => {
      console.log(progress);
      this.zone.run(() => {
        this.progress = progress;
      });
    }, null, () => { this.progress = null; });

    this.photoService.upload(this.vehicleId, nativeHelement.files[0]).subscribe(p => this.photos.push(p));
  }
}
