import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../_models/customer.model';
import { Booking } from '../_models/booking.model';
import {   Observable, Subject, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
@Injectable({
  providedIn: 'root'
})
export class CustomersService {

  customersAPI = environment.api + 'customer' ;
  private CustomersSubjcet$ = new BehaviorSubject<Customer[]>(null);
  public Customers$ = this.CustomersSubjcet$.asObservable();
  constructor(private http: HttpClient , private router: Router , private notifier: NotificationsService) {


   }
  public getCustomers() {
     this.http.get(this.customersAPI).subscribe( (data: Customer[]) => {
       console.log(data);
      this.CustomersSubjcet$.next(data);
    }, err => {
      console.log(err);
      
    });
  }

  getCustomerById(id) {

   var val =  this.CustomersSubjcet$.getValue();
   return val.find(e => e.id === id);
  }

  createCustomer(model) {
    this.http.post(this.customersAPI , model).subscribe( (result ) => {
      this.CustomersSubjcet$.next(model);
      this.notifier.success('Customer Created' , '' ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
    });
  }

  deleteCustomer(id) {
    this.http.delete(this.customersAPI + '/' + id).subscribe( e  => {
      
      this.notifier.success('Customer Deleted' , '' ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
      this.router.navigate(['/app/customers']);
    });
  }

  editCustomer(model: Customer) {
    this.http.put(this.customersAPI + '/' + model.id , model ).subscribe( e => {
      this.notifier.success('Customer Updated' , '' ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
      this.router.navigate(['/app/customers']);
      
    })
  }
}