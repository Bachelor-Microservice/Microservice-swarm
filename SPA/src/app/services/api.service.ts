import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient ) { }


  get(): Promise<any> {
    return this.http.get(environment.api)
    .toPromise()
    .then(res => {
      return res;
    })
    .catch(err => {
      console.log(err)
    });
  }



}
