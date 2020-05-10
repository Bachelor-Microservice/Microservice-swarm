import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { NgForm } from '@angular/forms';
import { UsermanagerService } from 'src/app/services/usermanager.service';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.css']
})
export class EmailComponent implements OnInit {

  claims: any;

  constructor(private authService: AuthService , private userManager: UsermanagerService , private notifier: NotificationsService) {
    this.claims = authService.identityClaims;
   }
  ngOnInit() {
  }


  Submit(form: NgForm) {
    let id = this.claims.sub;
    let model = {UserId: id , Email: form.value.Email  };
    this.userManager.UpdateEmail(model).subscribe( e => {
      this.notifier.success('Email updated' , 'The email shown as the current email will be updated once you login again' , {
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
    } , err => {
      this.notifier.alert('Something went wrong' , 'The email was not updated, please logout and try again' , {
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
    })
  }

}
