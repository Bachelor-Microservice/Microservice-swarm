import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CreateItemComponent } from './create-item/create-item.component';
import { Items } from 'src/app/_models/ItemEntity.model';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent implements OnInit {

  columnDefs = [
    {headerName: '#', field: 'id' },
    {headerName: 'Titel', field: 'name' },
    {headerName: 'Pris', field: 'price'},
    {headerName: 'Prismodel', field: 'priceModel'},
    {headerName: 'Varekode', field: 'itemCode'},
];

rowData = [
    { id: '10', name: 'Barn - 10 klip', price: 100.00, priceModel: 'Uden Tidsberegning' , itemCode: 'Salgsvare' },
    { id: '10', name: 'Barn - 10 klip', price: 100.00, priceModel: 'Uden Tidsberegning' , itemCode: 'Salgsvare' },
    { id: '10', name: 'Barn - 10 klip', price: 100.00, priceModel: 'Uden Tidsberegning' , itemCode: 'Salgsvare' },
];
  constructor(public dialog: MatDialog) { }

  createItem: Items;

  ngOnInit() {
    this.createItem = new Items();
  }

  openCreate() {
    const createMenuref = this.dialog.open(CreateItemComponent , {
      width: '30%',
      data: {item: this.createItem}
    });

    createMenuref.afterClosed().subscribe((result: Items) => {
      console.log(result);
    });
  }

}
