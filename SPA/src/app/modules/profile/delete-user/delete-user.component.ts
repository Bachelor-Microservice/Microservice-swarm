import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UsermanagerService } from 'src/app/services/usermanager.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<DeleteUserComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any , private usermanager: UsermanagerService , private router: Router) { }

  ngOnInit() {
  }


  close() {
    this.dialogRef.close();
  }

  delete() {
    this.usermanager.DeleteUser(this.data.id).subscribe(e => {
      this.dialogRef.close();
      localStorage.clear();
      window.location.replace(window.location.origin);
    });
  }
}
