import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Items } from '../_models/ItemEntity.model';
import { BehaviorSubject } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { NotificationsService } from 'angular2-notifications';

@Injectable({
  providedIn: 'root'
})
export class ItemService {


  itemManagerApi = environment.api + 'itemmanager' ;

  _items = new BehaviorSubject<Items[]>([]);


  constructor(private http: HttpClient, private authServive: AuthService , private notifier: NotificationsService) { }

  loadItems() {
    var headers_object = new HttpHeaders().set("Authorization", "Bearer " + this.authServive.accessToken);
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
    this.http.post(this.itemManagerApi , item).subscribe( e=> {
      this.loadItems();
      this.notifier.success('Item created successfully' , '' ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
    });
  }

  editItem(item) {
    this.http.put(this.itemManagerApi , item).subscribe( e=> {
      this.loadItems()
      this.notifier.success('Item changed sucessfully' , '' ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
      ;});
  }

  deleteItem(item) {
    var itemId = {id: item.id}
    this.http.delete(this.itemManagerApi + '/' + item.id).subscribe( e=> {this.loadItems();
      this.notifier.success('Item deleted sucessfully' , '' ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
    });

  }






}
