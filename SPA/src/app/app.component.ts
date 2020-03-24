import { Component, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent {

  temperature: any = []
  constructor(private http: HttpClient) {

  }

  GetData(){
    this.http.get('http://34.77.231.255/api/weather').subscribe(e => this.temperature = e);
  }
}
