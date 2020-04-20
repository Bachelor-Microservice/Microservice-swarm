import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-api',
  templateUrl: './api.component.html',
  styleUrls: ['./api.component.css']
})
export class ApiComponent implements OnInit {

  public priceCalender: any = null;


  constructor(private api: ApiService) { }

  ngOnInit() {
  }


  GetApi() {
    this.api.get().subscribe((res) =>
    {
      this.priceCalender = res;
      console.log(this.priceCalender);
    });
  }

}
