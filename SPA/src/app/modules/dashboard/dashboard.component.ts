import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from "@angular/forms";
import { HttpClient } from '@angular/common/http';
import { ChartType } from 'angular-google-charts';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  title = 'Antal bookinger';
  type='AreaChart';
  data = [
     ["Mandag", 2],
     ["Tirsdag", 4],
     ["Onsdag", 0],
     ["Torsdag", 9],
     ["Fredag", 2],
     ["Lørdag", 8],
      ["Søndag", 1]
  ];
   columnNames = ['Browser', 'Percentage'];
   options = {
   };
   width = 440;
   height = 300;


  ngOnInit(): void {
  }



}

