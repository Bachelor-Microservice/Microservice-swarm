import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ItemDayService {

  constructor(private http: HttpClient) { }

  pricecalendarApi = environment.api + 'itemday';

  AddItemDays(itemDayListDTO) {
    console.log(itemDayListDTO);
    // return this.http.post(this.pricecalendarApi , itemDayListDTO);
  }

}
