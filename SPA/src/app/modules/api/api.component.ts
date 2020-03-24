import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-api',
  templateUrl: './api.component.html',
  styleUrls: ['./api.component.css']
})
export class ApiComponent implements OnInit {

  public temperature: any;


  constructor(private api: ApiService) { }

  ngOnInit() {
  }


  GetApi() {
    this.api.get().then(res => this.temperature = res);
  }

}
