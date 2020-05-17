import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from "@angular/forms";
import { HttpClient } from '@angular/common/http';
import { ChartType } from 'angular-google-charts';
import { DashboardService } from 'src/app/services/dashboard.service';
import { Booking } from 'src/app/_models/booking.model';
import { BookingService } from 'src/app/services/booking.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  title = 'Antal bookinger';
  type = 'AreaChart';
  data = [];
  columnNames = ['Browser', 'Percentage'];
  options = {
  };
  width = 440;
  height = 300;

  arrivalsToday: Booking[] = [];
  departueToday: Booking[] = [];
  bookings: Booking[] = [];
  constructor(private bookingService: BookingService) { }

  ngOnInit(): void {
    this.bookingService.getBookings().subscribe((e: Booking[]) => {
      this.bookings = e;
      e.forEach(e => {
        if (new Date().getDate() === new Date(e.arrival).getDate()) {

          this.arrivalsToday.push(e);
        } else if (new Date().getDate() === new Date(e.depature).getDate()) {
          this.departueToday.push(e);
        }
      })
    });
    var days = ['Søndag','Mandag','Tirsdag','Onsdag','Torsdag','Fredag','Lørdag'];
    
  this.getCurrentWeek().forEach(date => {
    let arrival = 0;
    this.bookings.forEach( booking => {
      arrival = 0;
      if (date.getDate() === new Date(booking.arrival).getDate()) {
        arrival++;
      }
    })
    this.data.push([days[date.getDay()] , arrival])
  });
  
   
  }

  getCurrentWeek() {
    let dates = new Array<Date>();
    var today = new Date();
    var day = today.getDay() || 7; // Get current day number, converting Sun. to 7
    if( day !== 1 ) {
      today.setHours(-24 * (day - 1)); 
    }               // Only manipulate the date if it isn't Mon.
    
    for(let i = 0; i<7; i++) {
      var date = new Date(today);
      date.setDate(date.getDate() +i);
      dates.push(date);
    }
    return dates;
  }



 

  



}

