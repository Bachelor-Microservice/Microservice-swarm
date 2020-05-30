import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MailService {

  mailAPI = environment.api + 'email' ;

constructor(private http: HttpClient) { }


  public sendMailNewBooking(booking) {
    this.http.post(this.mailAPI + '/newbooking' , booking).subscribe(e => {
      console.log(e);
      
    });
  }

    public sendMailNewCustomer(customer) {
      this.http.post(this.mailAPI + '/newcustomer' , customer).subscribe(e => {
        console.log(e);
        
      });
  }


}
