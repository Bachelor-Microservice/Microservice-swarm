import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { NgForm } from '@angular/forms';
import { UsermanagerService } from 'src/app/services/usermanager.service';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.css']
})
export class PasswordComponent implements OnInit {

   claims: any;
   Status: string = '';
  constructor(private authService: AuthService , private usermanager: UsermanagerService , private notifier: NotificationsService) {
    this.claims = authService.identityClaims;
   }
  ngOnInit() {
  }


  Submit(form){
    let id = this.claims.sub;
    let model = {UserId: id , CurrentPassword: form.value.CurrentPassword , NewPassword: form.value.NewPassword };
    this.usermanager.UpdatePassword(model).subscribe( res => {
      this.Status = 'Success';
      form.resetForm();
      this.notifier.success('Password updated' , null , {
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
    } , err => {
      this.notifier.alert('Something went wrong' , 'The password was not updated, please logout and try again' , {
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
    });
  }

}
