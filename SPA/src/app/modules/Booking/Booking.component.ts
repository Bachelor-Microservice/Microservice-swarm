import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { startWith, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CustomersService } from 'src/app/services/customers.service';
import { MatStepper } from '@angular/material/stepper';
import { ItemService } from 'src/app/services/item.service';
import { Items } from 'src/app/_models/ItemEntity.model';


@Component({
  selector: 'app-Booking',
  templateUrl: './Booking.component.html',
  styleUrls: ['./Booking.component.css']
})
export class BookingComponent implements OnInit {

  myControl = new FormControl();
  options: string[] = [];
  filteredOptions: Observable<string[]>;
  isLinear = false;
  firstFormGroup: FormGroup;
  datesForm: FormGroup;
  firstStepDone = false;
  columnDefs;
  items: Items[];
  createCustomerForm: FormGroup;
  constructor(private _formBuilder: FormBuilder , private customerService: CustomersService , private itemService: ItemService) { 
    this.customerService.getCustomers();
  }

  ngOnInit() {

    this.columnDefs = [
      {field: 'id' },
    ]

    this.itemService.getItems().subscribe( e => {
      this.items = e;
    });

    this.options.push('New Customer');
    this.customerService.Customers$.subscribe(e => {
      console.log(e);
      e.forEach(element => {
        this.options.push(element.supplementName);
      });
    });
    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map((value: string) => this._filter(value))
      );
    this.firstFormGroup = this._formBuilder.group({
    });
    this.createCustomerForm = this._formBuilder.group({
      supplementName: ['', Validators.required],
      email: ['' , Validators.required],
      address: ['', Validators.required],
      telephonePrimary: ['' , Validators.required],
      mobilePhone: ['', Validators.required]
    });
    this.datesForm  = new FormGroup({
      arrival: new FormControl(null),
      departue: new FormControl(null),
    });
  }

  check() {
    console.log();
    
    console.log(this.datesForm.value);
    
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }

  GoNext(stepper: MatStepper) {
    console.log(this.myControl.value);
    stepper.next();
    if (this.myControl.value !== 'New Customer') {
      stepper.next();
    }
  }

}
