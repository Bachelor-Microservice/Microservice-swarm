import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Customer } from 'src/app/_models/customer.model';
import { CustomersService } from 'src/app/services/customers.service';

@Component({
  selector: 'app-detail-customer',
  templateUrl: './detail-customer.component.html',
  styleUrls: ['./detail-customer.component.css']
})
export class DetailCustomerComponent implements OnInit {
  columnDefs;
  public customer: Customer
  public editMode = false;
  constructor(private router: Router, private route: ActivatedRoute , private customerService: CustomersService) { }

  ngOnInit() {
    this.columnDefs = [
      {field: 'id' },
      {field: 'arrival'  },
      {field: 'depature' },
      {field: 'itemName' },
      {field: 'itemNo' },
  ];
    this.route.params.subscribe( params => {
      var id = params['id'];
      this.customer = this.customerService.getCustomerById(id);
      console.log(this.customer.id);
    })
  }

  onRowClicked(event) {
    console.log(event);
    
  }
  onEditCustomer() {
    this.editMode = true;
  }

  onDeleteCustomer() {
    
  }
  onApplyEditCustomer() {
    this.editMode = false;
  }

}
