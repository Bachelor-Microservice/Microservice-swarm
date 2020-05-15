import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { startWith, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CustomersService } from 'src/app/services/customers.service';
import { MatStepper } from '@angular/material/stepper';
import { ItemService } from 'src/app/services/item.service';
import { Items } from 'src/app/_models/ItemEntity.model';
import { PriceCalendarService } from 'src/app/services/priceCalendar.service';
import { ItemPriceAndCurrencyResponse } from 'src/app/_models/ItemPriceAndCurrencyResponse.model';
import { Item } from 'src/app/_models/Item.model';
import { ItemDayDTO } from 'src/app/_models/ItemDayDTO.model';


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
  itemPriceAndCurrencyResponse: any;
  chosenItemPriceAndCurrencyResponse: Item;
  bookingItems: any[] = [];
  columnDefs;
  BookedItem: Items;
  items: Items[] = [];
  createCustomerForm: FormGroup;
  constructor(private _formBuilder: FormBuilder , private customerService: CustomersService , private itemService: ItemService , private PricecalendarService: PriceCalendarService) { 
    this.customerService.getCustomers();
  }

  ngOnInit() {
   
    this.columnDefs = [
      {field: 'id' },
    ]

    this.itemService.getItems().subscribe( e => {
      e.forEach( item => {
        if (item.quickPost === true) {
          this.items.push(item);
        }
      })
    });
    this.PricecalendarService.getPriceCalendar().subscribe( (itemPriceAndCurrency) => {
      console.log(itemPriceAndCurrency);
      this.itemPriceAndCurrencyResponse = itemPriceAndCurrency.data;
      
    });

    this.options.push('New Customer');
    this.customerService.Customers$.subscribe(e => {
      console.log(e);
      if (e !== null) {
      e.forEach(element => {
        this.options.push(element.supplementName);
      });
    }
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

  onClickBooking(item , stepper: MatStepper) {
    this.BookedItem = item;
    stepper.next();
    this.itemPriceAndCurrencyResponse.forEach(element => {
    element.groups.forEach(group => {
      group.items.forEach( priceCalendarItem => {
        if (priceCalendarItem.groupId === item.articleGroup ) {
          this.chosenItemPriceAndCurrencyResponse = priceCalendarItem;
        }
      });
    });
  });
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

  GoNextDays(stepper: MatStepper) {
    this.CreateItems();
    stepper.next();
  }

  
  CreateItems() {
    

    this.itemPriceAndCurrencyResponse.forEach(element => {
      element.groups.forEach(group => {
        group.items.forEach( priceCalendarItem => {          
          var item = this.items.find( e => e.articleGroup === priceCalendarItem.groupId );
          if (item !== undefined) {
          console.log(item);
          let totalPrice = 0;
          const oneDay = 24 * 60 * 60 * 1000;
          let daysChoosen = Math.round(Math.abs((this.datesForm.value.arrival - this.datesForm.value.departue) / oneDay))+1;
          console.log("Neqw itemday");
          this.bookingItems.push({
              itemName: item.name,
              days: daysChoosen,
              arrival: this.datesForm.value.arrival,
              departue: this.datesForm.value.departue,
              standardPrice: item.price,
              itemDays: priceCalendarItem.itemDays
            });
          }
      });
    });
  });

    this.bookingItems.forEach(item => {
      item['totalPrice'] = 0;
      item['totalstandardPrice'] = 0;
      for (var d = new Date(this.datesForm.value.arrival); d <= this.datesForm.value.departue; d.setDate(d.getDate() + 1)) {
        let itemDay = item.itemDays.find(e => new Date(e.date).getDate() === d.getDate());
        console.log(itemDay);
        if (itemDay !== undefined) {
          item['totalPrice'] += +itemDay.price;
        }else {
          item['totalPrice'] += +item.standardPrice;
        }
        item['totalstandardPrice'] += +item.standardPrice ;
      }
   });
   console.log(this.bookingItems);
  }

}

/*

            }
*/
