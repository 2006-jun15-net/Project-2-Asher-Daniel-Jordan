import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { AppService } from './app.service';
import { AppRoutingModule } from './app-routing.module';
import { DoctorComponent } from './doctor/doctor.component';

@NgModule({
  declarations: [
    AppComponent,
    DoctorComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [AppService],
  bootstrap: [AppComponent]
})
export class AppModule { }
