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
import { BookingService } from 'src/app/services/booking.service';
import { CreateBookingDTO } from 'src/app/_models/CreateBookingDTO.model';
import { Customer } from 'src/app/_models/customer.model';
import { BookedDayDTO } from 'src/app/_models/BookedDayDTO.model';
import { MailService } from 'src/app/services/mail.service';


@Component({
  selector: 'app-Booking',
  templateUrl: './Booking.component.html',
  styleUrls: ['./Booking.component.css']
})
export class BookingComponent implements OnInit {

  myControl = new FormControl();
  customers: Customer[] = [];
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
  BookedItem: any;
  items: Items[] = [];
  createCustomerForm: FormGroup;
  constructor(private _formBuilder: FormBuilder , private customerService: CustomersService ,
     private itemService: ItemService , private PricecalendarService: PriceCalendarService ,
     private BookingService: BookingService , private MailService: MailService
     ) { 
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
       
        
      });
      console.log(this.items);
    });
    this.PricecalendarService.getPriceCalendar().subscribe( (itemPriceAndCurrency) => {
      console.log(itemPriceAndCurrency);
      this.itemPriceAndCurrencyResponse = itemPriceAndCurrency.data;
    });

    this.options.push('New Customer');
    this.customerService.Customers$.subscribe(e => {
      if (e !== null) {
        console.log("CUSTOMER");
        
        this.customers = e;
      e.forEach(element => {
        if (!this.options.find(e => e === element.supplementName))  {
          this.options.push(element.supplementName);
        }
        
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

    let customer = this.customers.find(e => e.supplementName === this.myControl.value);
    if (customer === undefined) {
      customer = {
        address: this.createCustomerForm.value.address,
        email: this.createCustomerForm.value.email,
        supplementName: this.createCustomerForm.value.supplementName,
        mobilePhone: this.createCustomerForm.value.mobilePhone,
        telephonePrimary: this.createCustomerForm.value.telephonePrimary,
        id: null,
        bookings: null,
        registrationDate: new Date(),
        type: 'customer'
      };
    }
    /*
    this.itemPriceAndCurrencyResponse.forEach(element => {
    element.groups.forEach(group => {
      group.items.forEach( priceCalendarItem => {
        if (priceCalendarItem.groupId === item.articleGroup ) {
          this.chosenItemPriceAndCurrencyResponse = priceCalendarItem;
        }
      });
    });
  });
  */
    let arrival = new Date(this.BookedItem.arrival);
    arrival.setHours(2);
    
    let departure = new Date(this.BookedItem.departue);
    departure.setHours(2);
    let createBooking: CreateBookingDTO = 
    {
      arrival: arrival.toISOString(),
      currency: this.BookedItem.currency,
      customerName: customer.supplementName,
      customerid: customer.id,
      bookedDays: this.getDates(this.BookedItem.arrival , this.BookedItem.departue , this.BookedItem.price),
      depature: departure.toISOString(),
      email: customer.email,
      itemDescription: this.BookedItem.description,
      itemName: this.BookedItem.itemName,
      itemNo: this.BookedItem.itemNo,
      price: this.BookedItem.totalPrice
    };
    this.BookingService.addBooking(createBooking);
    this.MailService.sendMailNewBooking(createBooking);
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
    console.log(this.items);
    this.itemPriceAndCurrencyResponse.forEach(element => {
      element.groups.forEach(group => {
        group.items.forEach( (priceCalendarItem: Item )=> {
          var item = this.items.find( e => e.itemNo === priceCalendarItem.id );
          if (item !== undefined) {
          let totalPrice = 0;
          const oneDay = 24 * 60 * 60 * 1000;
          let daysChoosen = Math.round(Math.abs((this.datesForm.value.arrival - this.datesForm.value.departue) / oneDay))+1;
          this.bookingItems.push({
              itemName: item.name,
              days: daysChoosen,
              arrival: this.datesForm.value.arrival,
              departue: this.datesForm.value.departue,
              standardPrice: item.price,
              itemDays: priceCalendarItem.itemDays,
              currency: element.currency,
              description: group.description,
              itemNo: item.itemNo
            });
          }
        
      });
    });
  });

  console.log('after sort');
  console.log(this.bookingItems);
  

    this.bookingItems.forEach(item => {
      item['totalPrice'] = 0;
      item['totalstandardPrice'] = 0;
      for (var d = new Date(this.datesForm.value.arrival); d <= this.datesForm.value.departue; d.setDate(d.getDate() + 1)) {
        let itemDay = item.itemDays.find(e => new Date(e.date).getDate() === d.getDate());
        console.log(itemDay);
        if (itemDay !== undefined) {
          item['totalPrice'] += +itemDay.price;
          item['price'] = +itemDay.price;
        }else {
          item['totalPrice'] += +item.standardPrice;
          item['price'] = +item.standardPrice;
        }
        item['totalstandardPrice'] += +item.standardPrice ;
      }
   });
   console.log(this.bookingItems);
  }

   getDates(startDate, stopDate , price) {
     let dateArray = new Array<BookedDayDTO>();
     for (var d = new Date(startDate); d <= stopDate; d.setDate(d.getDate() + 1)) {
      dateArray.push({
        date: d.toISOString(),
        discountDescription: 'ss',
        itemDayId: 0,
        priceForDay: price
      })
     }
     return dateArray;
}



}

