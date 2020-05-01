import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-signalR',
  templateUrl: './signalR.component.html',
  styleUrls: ['./signalR.component.css']
})
export class SignalRComponent implements OnInit {

  private _hubConnection: HubConnection;
  message = '';
  messages: string[] = [];
  hubConnectionString = environment.api + 'hub';
  constructor() { }

  ngOnInit() {
    
    this._hubConnection = new HubConnectionBuilder().withUrl(this.hubConnectionString)
    .build();

    this._hubConnection.on('Send', (data: any) => {
    const received = `Received: ${data}`;
    this.messages.push(received);
    console.log(data);
    
    });

    this._hubConnection.on('HELLO' , data => {
      console.log(data);
    });

    this._hubConnection.start()
    .then(() => {
      console.log('Hub connection started');
    })
    .catch(err => {
    console.log('Error while establishing connection');
    });
  }

  public sendMessage(): void {
    const data = `Sent: ${this.message}`;
    this._hubConnection.invoke('Send', data);
    this.messages.push(data);
  }

}
