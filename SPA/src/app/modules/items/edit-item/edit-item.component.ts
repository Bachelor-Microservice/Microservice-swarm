import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ItemService } from 'src/app/services/item.service';

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<EditItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any , private itemservice: ItemService) { }

  ngOnInit() {
    console.log(this.data);
  }

  onNoClick(): void {
    this.dialogRef.close(this.data);
  }

  deleteItem() {
    this.itemservice.deleteItem(this.data);
    this.dialogRef.close("DELETE");
  }

}
