import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {HttpClientModule} from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DefaultModule } from './layouts/default/default.module';
import { OAuthModule } from 'angular-oauth2-oidc';
import { SilentRefreshComponent } from './silent-refresh/silent-refresh.component';





@NgModule({
   declarations: [
      AppComponent,
      SilentRefreshComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      OAuthModule.forRoot(),
      BrowserAnimationsModule,
      DefaultModule.forRoot()
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
