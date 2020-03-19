import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  temperature: any = []
  constructor(private http: HttpClient) {

  }

  GetData(){
    this.http.get('http://localhost:5000/weather').subscribe(e => this.temperature = e);
  }
}
