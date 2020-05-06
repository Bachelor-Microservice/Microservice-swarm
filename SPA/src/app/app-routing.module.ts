import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DefaultComponent } from './layouts/default/default.component';
import { DashboardComponent } from './modules/dashboard/dashboard.component';
import { PostsComponent } from './modules/posts/posts.component';

import { PriceCalendarComponent } from './modules/price-calendar/price-calendar.component';
import { ItemsComponent } from './modules/items/items.component';
import { SignalRComponent } from './modules/signalR/signalR.component';
import { SilentRefreshComponent } from './silent-refresh/silent-refresh.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth/auth-guard.service';



const routes: Routes = [{
  path: 'app',
  component: DefaultComponent,
  canActivate: [AuthGuard],
  children: [
  {
    path: 'pricecalendar',
  component: PriceCalendarComponent,
  canActivate: [AuthGuard]
  },
  {
    path: 'items',
    component: ItemsComponent
  }, 
  {
    path: 'silent-refresh',
    component: SilentRefreshComponent
  },
  {
    path: 'signal',
    component: SignalRComponent
  },
  { path: '**', redirectTo: '' },
]
},
{
  path: '',
  component: HomeComponent
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
