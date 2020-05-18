import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Booking } from 'src/app/_models/booking.model';
import { BookingService } from 'src/app/services/booking.service';

@Component({
  selector: 'app-detail-booking',
  templateUrl: './detail-booking.component.html',
  styleUrls: ['./detail-booking.component.css']
})
export class DetailBookingComponent implements OnInit {
  columnDefs;
  booking: Booking;
  editMode = false;
  constructor(private route: ActivatedRoute , private bookingService: BookingService) { }

  ngOnInit() {
    this.route.params.subscribe( params => {
      var id = params['id'];
      this.booking = this.bookingService.getBookingById(id);
      console.log('Gettting booking');
      console.log(this.booking);
      
      
    });
    this.columnDefs = [
      {field: 'date'  },
      {field: 'priceForDay' },
      {field: 'discountDescription' },
  ];
  }

  onRowClicked(event) {
  }

  onEditCustomer() {
    this.editMode = true;
  }

  onDeleteCustomer() {
    //
    this.bookingService.deleteBooking(this.booking);
  }
  onApplyEditCustomer() {
    //this.customerService.editCustomer(this.customer);
    this.bookingService.updateBooking(this.booking);
    this.editMode = false;
  }

}
