import { Component, OnInit, inject, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup } from '@angular/forms';
import { Items } from 'src/app/_models/ItemEntity.model';

interface Food {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-create-item',
  templateUrl: './create-item.component.html',
  styleUrls: ['./create-item.component.css']
})
export class CreateItemComponent implements OnInit {

  groups: any[] = [];
  currency: any[] = [];
  newGroup = false;
  itemObject: any;
  constructor(
    public dialogRef: MatDialogRef<CreateItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
      this.currency.push({currency: 'Create new currency' , id: 0 , groups: []})
      data.data.data.forEach((currency: any) =>  {
        this.currency.push({currency: currency.currency , id: currency.id , groups: currency.groups});
      });
      console.log(this.currency);
      
    }


  onNoClick(): void {
    this.dialogRef.close(this.data);
  }
  ngOnInit() {
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
