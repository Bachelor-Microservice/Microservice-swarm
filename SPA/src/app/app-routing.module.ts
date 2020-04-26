import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DefaultComponent } from './layouts/default/default.component';
import { DashboardComponent } from './modules/dashboard/dashboard.component';
import { PostsComponent } from './modules/posts/posts.component';

import { PriceCalendarComponent } from './modules/price-calendar/price-calendar.component';
import { ItemsComponent } from './modules/items/items.component';


const routes: Routes = [{
  path: '',
  component: DefaultComponent,
  children: [
    {
      pathMatch: 'full',
      path: '',
      redirectTo: 'pricecalendar'
    },
  {
    path: 'pricecalendar',
  component: PriceCalendarComponent
  },
  {
    path: 'items',
    component: ItemsComponent
  }
]
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
