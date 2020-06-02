import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { Booking } from '../_models/booking.model';
import { HttpClient } from '@angular/common/http';
import { CreateBookingDTO } from '../_models/CreateBookingDTO.model';
import { NotificationsService } from 'angular2-notifications';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  private bookings$ = new BehaviorSubject<Booking[]>([]);

  private bookingAPI = environment.api + 'booking';

  constructor(private http: HttpClient , private notifier: NotificationsService, private router: Router) {
  }

  loadBookings() {
    this.http.get(this.bookingAPI).subscribe((data: Booking[]) => {
      this.bookings$.next(data);
      console.log(data);
    });
  }

  getBookings(): Observable<Booking[]>{
    this.loadBookings();

    return this.bookings$.asObservable();
  }

  addBooking(createdBooking: CreateBookingDTO) {
    console.log(createdBooking);
    this.http.post(this.bookingAPI , createdBooking).subscribe(e => {
      this.loadBookings();
    });
  }

  getBookingById(id) {
    let booking = this.bookings$.getValue().find(e => e.id === id);
    
    return booking;
  }

  updateBooking(booking: Booking) {
    console.log(booking);
    this.http.put(this.bookingAPI + '/' + booking.id , booking).subscribe(e => {
      this.loadBookings();
      this.notifier.success('Booking updated successfully' , '' ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
      this.router.navigate(['/app/bookings']);
    });
  }

  deleteBooking(booking: Booking) {
    this.http.delete(this.bookingAPI + '/' + booking.id).subscribe(e => {
      this.loadBookings();
      this.notifier.success('Booking deleted successfully' , '' ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
      this.router.navigate(['/app/bookings']);
    });
  }
  }
