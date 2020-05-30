import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Booking } from 'src/app/_models/booking.model';
import { CustomersService } from 'src/app/services/customers.service';
import { MailService } from 'src/app/services/mail.service';

@Component({
  selector: 'app-create-customer',
  templateUrl: './create-customer.component.html',
  styleUrls: ['./create-customer.component.css']
})
export class CreateCustomerComponent implements OnInit {

  constructor(private customerService: CustomersService , private mailService: MailService) { }
  createCustomerForm = new FormGroup({
    supplementName: new FormControl(''),
    email: new FormControl(''),
    address: new FormControl(''),
    telephonePrimary: new FormControl(''),
    mobilePhone: new FormControl('')
  });
  ngOnInit() {
  }

  submitForm() {
    console.log(this.createCustomerForm);
    let createCustomerCommand = this.createCustomerForm.value;
    createCustomerCommand['bookings'] =  [];
    createCustomerCommand['type'] = 'Kunde';
    createCustomerCommand['registrationDate'] = new Date();
    console.log(createCustomerCommand);
    this.customerService.createCustomer(createCustomerCommand);
    this.mailService.sendMailNewCustomer(createCustomerCommand);
  }

}
