import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HubconnectorService {

  private ExcelEventRecievedSubject$ = new BehaviorSubject<any>(null);
  public ExcelEventRecieved$ = this.ExcelEventRecievedSubject$.asObservable();
private _hubConnection: signalR.HubConnection;
constructor() { 

  this._hubConnection = new signalR.HubConnectionBuilder().withUrl(environment.api + 'hub' , {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets
  }).configureLogging(signalR.LogLevel.Information)
  .build();

  this._hubConnection.on('Excel' , data => {
    this.ExcelEventRecievedSubject$.next(data);
  });

}

  public OpenConnection() {
    this._hubConnection.start()
    .then(() => {
      console.log('Hub connection started');
    })
    .catch(err => {
    console.log('Error while establishing connection');
    });
  }




}
