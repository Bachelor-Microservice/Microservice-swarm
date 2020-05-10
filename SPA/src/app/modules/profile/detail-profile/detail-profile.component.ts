import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-detail-profile',
  templateUrl: './detail-profile.component.html',
  styleUrls: ['./detail-profile.component.css']
})
export class DetailProfileComponent implements OnInit {

  claims: any;

  constructor(private authService: AuthService) {
    this.claims = authService.identityClaims;
   }

  ngOnInit() {
  }

}
