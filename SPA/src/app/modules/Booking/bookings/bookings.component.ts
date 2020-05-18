import { Component, OnInit } from '@angular/core';
import { BookingService } from 'src/app/services/booking.service';
import { Observable } from 'rxjs';
import { Booking } from 'src/app/_models/booking.model';
import { Router } from '@angular/router';
import moment from "moment";

@Component({
  selector: 'app-bookings',
  templateUrl: './bookings.component.html',
  styleUrls: ['./bookings.component.css']
})
export class BookingsComponent implements OnInit {

  columnDefs;
  bookings$: Booking[];
  constructor(private bookingService: BookingService , private router: Router) {
     this.bookingService.getBookings().subscribe( e => {
      console.log(e);
      this.bookings$ = e;
      
    });
   }

  ngOnInit() {
    this.columnDefs = [
      {field: 'arrival' , cellRenderer: (data) => {
        return data.value ? (new Date(data.value)).toLocaleDateString() : '';
    } },
      {field: 'depature' , cellRenderer: (data) => {
        return data.value ? (new Date(data.value)).toLocaleDateString() : '';
   }  },
      {field: 'price' },
      {field: 'customerName' },
      {field: 'email' },
      {field: 'itemName' },
  ];
  }

  dateFormatter(params) {
    return moment(params.value).format('DD/MM/YYYY');
  }

  onRowClicked(event) {
    console.log(event);
    this.router.navigate(['/app/bookings/' + event.data.id]);
  }

}
