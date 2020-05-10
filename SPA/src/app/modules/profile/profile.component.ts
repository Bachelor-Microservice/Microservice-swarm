import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { UsermanagerService } from 'src/app/services/usermanager.service';
import { MatDialog } from '@angular/material/dialog';
import { DeleteUserComponent } from './delete-user/delete-user.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {



  claims: any =  {};
  isAdmin: boolean;

  constructor(private authService: AuthService  , public dialog: MatDialog) { }

  ngOnInit() {
    this.claims = this.authService.identityClaims;
    this.isAdmin = this.authService.isAdmin();
  }

  deleteUser() {
    var id = this.claims.sub;
    
    const dialogRef = this.dialog.open(DeleteUserComponent, {
      width: '400px',
      disableClose: true ,
      data: { id: id }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      
    });

  }


}
