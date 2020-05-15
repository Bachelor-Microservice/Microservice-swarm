import { Component, OnInit } from '@angular/core';
import { CustomersService } from 'src/app/services/customers.service';
import { Customer } from 'src/app/_models/customer.model';
import { rowData } from '../price-calendar/price-calendar.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-Customer',
  templateUrl: './Customer.component.html',
  styleUrls: ['./Customer.component.css']
})
export class CustomerComponent implements OnInit {

  columnDefs;
  customers: Customer[];
  constructor(private customersService: CustomersService, private router: Router) { }

  ngOnInit() {
    this.columnDefs = [
      {field: 'id' },
      {field: 'supplementName' , filter: 'agTextColumnFilter' },
      {field: 'type' },
      {field: 'registrationDate' },
      {field: 'email' },
      {field: 'address' },
      {field: 'telephonePrimary' },
      {field: 'mobilePhone' },
  ];
    //this.customers.push({address: 'testaddress' })

    this.customersService.Customers$.subscribe( data => {
      this.customers = data;
    });
    this.customersService.getCustomers();
  }
  onRowClicked(event) {
    this.router.navigate(['/app/customers/' + event.data.id]);
  }

}
