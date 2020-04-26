import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Items } from '../_models/ItemEntity.model';
import { BehaviorSubject } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ItemService {


  itemManagerApi = environment.api + 'itemmanager' ;

  _items = new BehaviorSubject<Items[]>([]);


  constructor(private http: HttpClient) { }

  loadItems() {
    this.http.get(this.itemManagerApi ).pipe(
      map((res: any) => {
        return res.data;
      }) , tap( res => {
        this._items.next(res);
      })
    ).subscribe();
  }

  getItems() {
    this.loadItems();
    return this._items.asObservable();
  }

  addItem(item: any) {
    this.http.post(this.itemManagerApi , item).subscribe( e=> {this.loadItems();});
  }

  editItem(item) {
    this.http.put(this.itemManagerApi , item).subscribe( e=> {this.loadItems();});
  }

  deleteItem(item) {
    var itemId = {id: item.id}
    this.http.delete(this.itemManagerApi + '/' + item.id).subscribe( e=> {this.loadItems();});
  }






}
