import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private dashboardAPI = environment.api + 'dashboard';

  constructor(private http: HttpClient) {
    
  }

  getArrivalsToday() {
    return this.http.get(this.dashboardAPI + '/arrival');
  }


}
