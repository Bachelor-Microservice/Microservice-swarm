import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ItemPriceAndCurrencyResponse } from '../_models/ItemPriceAndCurrencyResponse.model';
import { ItemPriceDTO } from '../_models/itemPriceDTO.model';

@Injectable({
  providedIn: 'root'
})
export class PriceCalendarService {

  pricecalendarApi = environment.api + 'pricecalendar' ;

  constructor(private http: HttpClient) { }

  getPriceCalendar() {
    return this.http.get<ItemPriceDTO>(this.pricecalendarApi);
  }

  getPriceCalendarInInterval(interval) {
    return this.http.get(this.pricecalendarApi);
  }


}
