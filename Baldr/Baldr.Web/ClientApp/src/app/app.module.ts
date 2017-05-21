import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { LoggerService } from 'core/services/logger.service'

import { InstitutionListComponent } from './institution/institution-list.component';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
      AppComponent,
      InstitutionListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [
      LoggerService
  ],
  bootstrap: [
      AppComponent
  ]
})
export class AppModule { }
