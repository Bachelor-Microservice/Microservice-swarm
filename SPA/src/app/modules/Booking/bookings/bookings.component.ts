import { Component, OnInit } from '@angular/core';
import { BookingService } from 'src/app/services/booking.service';
import { Observable } from 'rxjs';
import { Booking } from 'src/app/_models/booking.model';
import { Router } from '@angular/router';

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
      {field: 'arrival'  },
      {field: 'depature' },
      {field: 'price' },
      {field: 'customerName' },
      {field: 'email' },
      {field: 'itemName' },
  ];
  }

  onRowClicked(event) {
    console.log(event);
    this.router.navigate(['/app/bookings/' + event.data.id]);
  }

}
