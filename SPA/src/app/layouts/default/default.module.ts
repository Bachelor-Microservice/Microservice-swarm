import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefaultComponent } from './default.component';
import { DashboardComponent } from 'src/app/modules/dashboard/dashboard.component';
import { RouterModule } from '@angular/router';
import { PostsComponent } from 'src/app/modules/posts/posts.component';
import { SharedModule } from 'src/app/shared/shared.module';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatDividerModule} from '@angular/material/divider';
import { SidenavService } from 'src/app/services/sidenav.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatCardModule} from '@angular/material/card';
import { ReactiveFormsModule } from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import { AgGridModule } from 'ag-grid-angular';
import { PriceCalendarComponent } from 'src/app/modules/price-calendar/price-calendar.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import {MatInputModule} from '@angular/material/input';
import { ItemsComponent } from 'src/app/modules/items/items.component';
import {MatDialogModule} from '@angular/material/dialog';
import { CreateItemComponent } from 'src/app/modules/items/create-item/create-item.component';
import { FormsModule } from '@angular/forms';
import { EditItemComponent } from 'src/app/modules/items/edit-item/edit-item.component';
import { SignalRComponent } from 'src/app/modules/signalR/signalR.component';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { ExcelDownloadComponent } from 'src/app/modules/price-calendar/ExcelDownload/ExcelDownload.component';
import { OAuthStorage, AuthConfig, OAuthModuleConfig, ValidationHandler, OAuthService } from 'angular-oauth2-oidc';
import { authConfig } from 'src/app/auth/auth-config';
import { JwksValidationHandler } from 'angular-oauth2-oidc-jwks';
import { HomeComponent } from 'src/app/home/home.component';
import { AuthGuard } from 'src/app/auth/auth-guard.service';
export function storageFactory(): OAuthStorage {
  return localStorage;
}

@NgModule({
  declarations: [
    DefaultComponent,
    DashboardComponent,
    PostsComponent,
    PriceCalendarComponent,
    ItemsComponent,
    CreateItemComponent,
    EditItemComponent,
    SignalRComponent,
    ExcelDownloadComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    MatNativeDateModule ,
    MatInputModule,
    MatFormFieldModule,
    MatDatepickerModule,
    SharedModule,
    MatSidenavModule,
    MatDividerModule,
    BrowserAnimationsModule,
    SimpleNotificationsModule.forRoot(),
    MatCardModule,
    ReactiveFormsModule,
    MatButtonModule,
    AgGridModule.withComponents([])
  ],
  providers: [ SidenavService , AuthGuard],
})
export class DefaultModule { 
  static forRoot(): ModuleWithProviders<DefaultModule> {
    return {
      ngModule: DefaultModule,
      providers: [
        { provide: AuthConfig, useValue: authConfig },
        { provide: OAuthModuleConfig, useValue: authConfig },
        { provide: OAuthStorage, useFactory: storageFactory },
      ]
    };
  }

}
