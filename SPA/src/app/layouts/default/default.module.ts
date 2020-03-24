import { NgModule } from '@angular/core';
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
import { ApiComponent } from 'src/app/modules/api/api.component';
import {MatButtonModule} from '@angular/material/button';
import { ApiService } from 'src/app/services/api.service';


@NgModule({
  declarations: [
    DefaultComponent,
    DashboardComponent, 
    PostsComponent,
    ApiComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    MatSidenavModule,
    MatDividerModule,
    BrowserAnimationsModule,
    MatCardModule,
    ReactiveFormsModule,
    MatButtonModule
  ],
  providers: [ SidenavService, ApiService ],
})
export class DefaultModule { }
