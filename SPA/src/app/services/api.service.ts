import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { ItemPriceAndCurrencyResponse } from '../_models/ItemPriceAndCurrencyResponse.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient ) { }

  pricecalendarApi = environment.api ;

  get(): Observable<ItemPriceAndCurrencyResponse> {
    console.log(this.pricecalendarApi);
    return this.http.get<ItemPriceAndCurrencyResponse>(this.pricecalendarApi);
  }

}
