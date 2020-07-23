import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import Doctor from './models/doctor';


@Injectable({
  providedIn: 'root'
})
export class AppService {

  private serviceUrl = 'https://localhost:44362/';
  //private serviceUrl = 'https://project2-hospital.azurewebsites.net/';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json'})
  };

  constructor(private http: HttpClient) {  }
/*
  //grab data from the backend
  getDoctors(): Observable<Doctor[]>{
    return this.http.get<Doctor[]>(this.serviceUrl)
    .pipe(
     // tap(_ => this.log('fetched doctors')),
      catchError(this.handleError<Doctor[]>('getDoctors', []))
    )
  }
*/
  //promise
  getDoctors(){
    return this.http.get<Doctor[]>(`${this.serviceUrl}/api/Doctors`)
      .toPromise();
  }

  /**
   * The below is copied from hero stuff
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      //this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
  
  /** Log a HeroService message with the MessageService */
 // private log(message: string) {
 //   this.messageService.add(`HeroService: ${message}`);
  //}

}
