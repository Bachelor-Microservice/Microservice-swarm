import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UsermanagerService } from 'src/app/services/usermanager.service';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {


  constructor(private usermanager: UsermanagerService, private notifier: NotificationsService) { }

  ngOnInit() {
  }

  Submit(form: NgForm) {
    let model = {Username: form.value.Username , Email: form.value.Email , Password: form.value.Password};
    this.usermanager.createUser(model).subscribe( e=> {
      console.log(e);
      form.resetForm();
      this.notifier.success('User created Succesfully' , 'The user can now use the login to enter the site' ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
    } , err => {
      this.notifier.alert('Something went wrong' , 'The user was not created, please logout and try again' , {
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
    });
    
  }

}
