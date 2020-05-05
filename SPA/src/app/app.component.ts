import { Component, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OAuthService } from 'angular-oauth2-oidc';
import { authConfig } from './auth/auth-config';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent {

  temperature: any = []
  constructor(private http: HttpClient, private authService: OAuthService) {
    this.authService.configure(authConfig);
    this.authService.loadDiscoveryDocument().then( e => {
      console.log(e);
    });
  }

  GetData(){
    this.http.get('http://34.77.231.255/api/weather').subscribe(e => this.temperature = e);
  }
}
