import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-signalR',
  templateUrl: './signalR.component.html',
  styleUrls: ['./signalR.component.css']
})
export class SignalRComponent implements OnInit {

  private _hubConnection: HubConnection;
  message = '';
  messages: string[] = [];
  messageResult;
  hubConnectionString = environment.api + 'hub';
  constructor(private http: HttpClient) { }

  ngOnInit() {
   
  }

  getExcel() {
    
  }


  public sendMessage(): void {
    const data = `Sent: ${this.message}`;
    this._hubConnection.invoke('Send', data);
    this.messages.push(data);
  }

}
