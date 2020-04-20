import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ItemPriceAndCurrencyResponse } from '../_models/ItemPriceAndCurrencyResponse.model';

@Injectable({
  providedIn: 'root'
})
export class PriceCalendarService {

  pricecalendarApi = environment.api + 'pricecalender' ;

  constructor(private http: HttpClient) { }

  getPriceCalendar() {
    return this.http.get<ItemPriceAndCurrencyResponse>(this.pricecalendarApi);
  }

  getPriceCalendarInInterval(interval) {
    return this.http.post(this.pricecalendarApi , interval);
  }


}
