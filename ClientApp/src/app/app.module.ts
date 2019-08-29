
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { MakeService } from './services/make.service';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VeichleFormComponent } from './veichle/veichle-form.component';
import { ModelService } from './services/model.service';
import { FeaturesService } from './services/features.service';
import { AppErroHandler } from './app-error-handler';
import { VeichleService } from './services/veichle.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VeichleFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'veichle/new', component: VeichleFormComponent },
      { path: 'veichle/:id', component: VeichleFormComponent },
    ])
  ],
  providers: [
    MakeService,
    ModelService,
    FeaturesService,
    VeichleService,
    { provide: ErrorEvent, useClass: AppErroHandler }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
