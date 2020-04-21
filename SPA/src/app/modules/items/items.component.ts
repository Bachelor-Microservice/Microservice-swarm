import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CreateItemComponent } from './create-item/create-item.component';
import { SimpleItem } from 'src/app/_models/SimpleItem.model';

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

  createItem: SimpleItem;

  ngOnInit() {
    this.createItem = new SimpleItem();
  }

  openCreate() {
    const createMenuref = this.dialog.open(CreateItemComponent , {
      width: '70%',
      data: {item: this.createItem}
    });

    createMenuref.afterClosed().subscribe(result => {
      console.log(result);
    });

    createMenuref.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
      
    });
  }

}
