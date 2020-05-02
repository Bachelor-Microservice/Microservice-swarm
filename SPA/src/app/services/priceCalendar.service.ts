import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ItemPriceAndCurrencyResponse } from '../_models/ItemPriceAndCurrencyResponse.model';
import { ItemPriceDTO } from '../_models/itemPriceDTO.model';
import { ItemDayDTO } from '../_models/ItemDayDTO.model';

@Injectable({
  providedIn: 'root'
})
export class PriceCalendarService {

  pricecalendarApi = environment.api + 'pricecalendar/';

  constructor(private http: HttpClient) { }

  getPriceCalendar() {
    return this.http.get<ItemPriceDTO>(this.pricecalendarApi);
  }

  getPriceCalendarInInterval(interval) {
    return this.http.get(this.pricecalendarApi);
  }

  getExcel(formData) {
    console.log(formData);
    let params = new HttpParams();
    params = params.append('from' , formData.from.toISOString());
    params = params.append('to' , formData.to.toISOString());
    return this.http.get(this.pricecalendarApi + 'excel'  , {responseType: 'blob' , params: params});
  }


}
