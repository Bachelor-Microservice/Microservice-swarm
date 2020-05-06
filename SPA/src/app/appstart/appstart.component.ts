import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-appstart',
  templateUrl: './appstart.component.html',
  styleUrls: ['./appstart.component.css']
})
export class AppstartComponent implements OnInit {

 
  constructor(private authService: AuthService , private router: Router) { 

  }

  ngOnInit() {

  }



}
