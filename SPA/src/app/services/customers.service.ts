import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../_models/customer.model';
import { Booking } from '../_models/booking.model';
import {   Observable, Subject, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class CustomersService {

  customersAPI = environment.api + 'customer' ;
  private CustomersSubjcet$ = new BehaviorSubject<Customer[]>(null);
  public Customers$ = this.CustomersSubjcet$.asObservable();
  constructor(private http: HttpClient) {

    let booking1: Booking = {
      arrival: new Date,
      depature: new Date,
      id: '123-3434',
      itemName: 'Hytte1',
      itemNo: '5342'
    }
    var bookings: Booking[] = [];
    bookings.push(booking1);
    let customer1: Customer = {
      address: 'testAddress',
      email: 'test@test.dk',
      id: '1243-24322',
      mobilePhone: '+4545454545',
      registrationDate: new Date(),
      supplementName: 'Peter',
      telephonePrimary: 'telephone',
      type: 'customer1',
      bookings: bookings
    };
    
    let customer2: Customer = {
      address: 'testAddress',
      email: 'test@test.dk',
      id: '125543-24322',
      mobilePhone: '+4545454545',
      registrationDate: new Date(),
      supplementName: 'Frederik',
      telephonePrimary: 'telephone',
      type: 'customer2',
      bookings: bookings
    };

    
    //this.customers.push(customer1);
    //this.customers.push(customer2);
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
    });
  }

  deleteCustomer(id) {
   // this.http.delete(this.customersAPI)
  }

 
}
