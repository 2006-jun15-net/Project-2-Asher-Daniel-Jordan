import { Component, OnInit } from '@angular/core';
import { AppService } from '../app.service';
import Doctor from '../models/doctor';


@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent implements OnInit {

  doctors: Doctor[] | null = null;
  error: string | null = null;


  reloadDoctors(){
    //or promise
    return this.databaseDoctor.getDoctors()
    .then(items => {
      this.error = null;
      this.doctors = items;
    })
    .catch(error => this.error = error.toString());
  }
/*
  getDoctors(): void {
    this.databaseDoctor.getDoctors()
    .subscribe(doctors => this.doctors = doctors)
  }
*/
  constructor(private databaseDoctor: AppService) { }

  ngOnInit(): void {
    //this.getDoctors()
    this.reloadDoctors().then();
    //observable

      
  }

}
