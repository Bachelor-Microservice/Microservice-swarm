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
import { AppstartComponent } from 'src/app/appstart/appstart.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { TestComponent } from 'src/app/home/test/test.component';
import { authModuleConfig } from 'src/app/auth/authModuleConfig';
import { ProfileComponent } from 'src/app/modules/profile/profile.component';
import {MatListModule} from '@angular/material/list';
import { PasswordComponent } from 'src/app/modules/profile/password/password.component';
import { DetailProfileComponent } from 'src/app/modules/profile/detail-profile/detail-profile.component';
import { EmailComponent } from 'src/app/modules/profile/email/email.component';
import { CreateUserComponent } from 'src/app/modules/profile/create-user/create-user.component';
import { UsersComponent } from 'src/app/modules/profile/users/users.component';
import { DeleteUserComponent } from 'src/app/modules/profile/delete-user/delete-user.component';
import {MatBadgeModule} from '@angular/material/badge';
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
    HomeComponent,
    AppstartComponent,
    TestComponent,
    ProfileComponent,
    PasswordComponent,
    DetailProfileComponent,
    EmailComponent,
    CreateUserComponent,
    UsersComponent,
    DeleteUserComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    MatNativeDateModule ,
    MatInputModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
    MatDatepickerModule,
    SharedModule,
    MatSidenavModule,
    MatBadgeModule,
    MatListModule,
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
      {provide : OAuthModuleConfig, useValue: authModuleConfig}
      ]
    };
  }

}
