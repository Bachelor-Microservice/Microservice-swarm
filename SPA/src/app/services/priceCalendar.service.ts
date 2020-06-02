import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ItemPriceAndCurrencyResponse } from '../_models/ItemPriceAndCurrencyResponse.model';
import { ItemPriceDTO } from '../_models/itemPriceDTO.model';
import { ItemDayDTO } from '../_models/ItemDayDTO.model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PriceCalendarService {
  pricecalendarApi = environment.api + 'pricecalendar';
  private ItemAndCurrencyResponse$ = new BehaviorSubject<ItemPriceDTO[]>([]);

  constructor(private http: HttpClient) { }

  LoadPriceCalendar() {
    return this.http.get(this.pricecalendarApi).subscribe((data: any) => {
      console.log(data);
      this.ItemAndCurrencyResponse$.next(data.data);
    });
  }

  getPriceCalendar() {
    this.LoadPriceCalendar();
    return this.ItemAndCurrencyResponse$.asObservable();
  }

  getPriceCalendarInInterval() {
    return this.http.get(this.pricecalendarApi);
  }

  getExcel(formData) {
    console.log(formData);
    let params = new HttpParams();
    params = params.append('from' , formData.from.toISOString());
    params = params.append('to' , formData.to.toISOString());
    return this.http.get(this.pricecalendarApi + '/excel'  , {responseType: 'blob' , params: params});
  }

  getPriceAndCurrencyWithoutItems()
  {
    return this.http.get(this.pricecalendarApi + '/allwithoutitems' );
  }


}
