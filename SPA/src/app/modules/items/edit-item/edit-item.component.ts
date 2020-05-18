import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ItemService } from 'src/app/services/item.service';

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent implements OnInit {

  groups: any[] = [];
  currency: any[] = [];
  newGroup = false;
  itemObject: any;
  constructor(public dialogRef: MatDialogRef<EditItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any , @Inject(MAT_DIALOG_DATA) public item: any  , private itemservice: ItemService) {
      console.log(item);
      
      this.currency.push({currency: 'Create new currency' , id: 0 , groups: []})
      data.data.data.forEach((currency: any) =>  {
        this.currency.push({currency: currency.currency , id: currency.id , groups: currency.groups});
      });
      console.log(this.currency);
     }

  ngOnInit() {
    console.log(this.data);
  }

  onNoClick(): void {
    this.dialogRef.close(this.data.item);
  }

  deleteItem() {
    console.log(this.data.item);
    this.itemservice.deleteItem(this.data.item);
    this.dialogRef.close("DELETE");
  }
  SelectChanged(event) {
    console.log(event);
    if (event.value === 'Create new currency') {
      this.newGroup = true;
      console.log('000');
    }
    this.currency.forEach(el => {
      if (el.currency === event.value) {
        this.groups = [];
        this.groups = el.groups;
       }
    });
  }

  OnChooseExistingGroup() {
    this.newGroup = false;
  }


}
