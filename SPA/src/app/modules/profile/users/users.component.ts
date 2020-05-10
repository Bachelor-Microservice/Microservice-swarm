import { Component, OnInit } from '@angular/core';
import { UsermanagerService } from 'src/app/services/usermanager.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {


  users: any[];

  constructor(private usermanager: UsermanagerService) { }

  ngOnInit() {
    this.usermanager.GetAllUsers().subscribe( (e: any[]) => {
      this.users = e;
      console.log(e);
    });
  }

}
