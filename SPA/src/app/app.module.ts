import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {HttpClientModule} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DefaultModule } from './layouts/default/default.module';
import { OAuthModule } from 'angular-oauth2-oidc';
import { SilentRefreshComponent } from './silent-refresh/silent-refresh.component';

import { environment } from 'src/environments/environment';
import { AkitaNgDevtools } from '@datorama/akita-ngdevtools';




@NgModule({
   declarations: [
      AppComponent,
      SilentRefreshComponent
   ],
   imports: [ 
      AkitaNgDevtools,
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      OAuthModule.forRoot(),
      BrowserAnimationsModule,
      DefaultModule.forRoot(),
     
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
