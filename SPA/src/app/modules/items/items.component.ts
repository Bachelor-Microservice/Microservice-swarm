import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CreateItemComponent } from './create-item/create-item.component';
import { Items } from 'src/app/_models/ItemEntity.model';
import { ItemService } from 'src/app/services/item.service';
import { rowData } from '../price-calendar/price-calendar.component';
import { EditItemComponent } from './edit-item/edit-item.component';
import { PriceCalendarService } from 'src/app/services/priceCalendar.service';
import { ItemPriceAndCurrencyResponse } from 'src/app/_models/ItemPriceAndCurrencyResponse.model';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent implements OnInit {

  columnDefs = [
    {headerName: '#', field: 'id' ,  width: 25},
    {headerName: 'Titel', field: 'name' },
    {headerName: 'Enhed', field: 'unit' },
    {headerName: 'Pris', field: 'price'},
    {headerName: 'Prismodel', field: 'priceModel'},
    {headerName: 'Varekode', field: 'itemCode'},
    {headerName: 'Varegruppe', field: 'itemNo'},
];

private gridApi;
private gridColumnApi;
private gridOptions;

  items: Items[];
  rowData: itemRowData[];
  ItemPriceAndCurrency: ItemPriceAndCurrencyResponse[];


  constructor(public dialog: MatDialog, private itemService: ItemService , private priceeCalendarService: PriceCalendarService) { }

  createItem: Items;

  ngOnInit() {
    this.createItem = new Items();
    this.itemService.getItems().subscribe((res: any) => {
      this.items = res;
      this.initTable();
    });
    this.priceeCalendarService.getPriceAndCurrencyWithoutItems().subscribe((e: ItemPriceAndCurrencyResponse[]) => {
      this.ItemPriceAndCurrency = e;
      console.log(e);
    });
  }


  initTable() {
    this.rowData = [];
    this.items.forEach((e: Items) => {
      this.rowData.push(
        {id: e.id , name: e.name , unit: e.unit , price: e.price , priceModel: e.priceModel , itemCode: e.itemCode , itemNo: e.itemNo }
        );
    });
  }


  openCreate() {
    const createMenuref = this.dialog.open(CreateItemComponent , {
      width: '30%',
      data: {data: this.ItemPriceAndCurrency}
    });

    createMenuref.afterClosed().subscribe((result) => {
      console.log(result);
      if (typeof result.PriceModelFrom === 'object') {
        let priceModelObjet = result.PriceModelFrom;
        result.articleGroup = priceModelObjet['currencyId'];
        result.priceModelFrom = priceModelObjet['description'];
        result.relationNo = priceModelObjet['id'];
     }else {
       console.log(result);
     }
     //this.itemService.addItem(result);
     if (result.quickPost === undefined) {
       result.quickPost = false;
     };
     console.log(result);
    });
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridOptions = this.gridColumnApi.columnController.gridOptionsWrapper.gridOptions;
    this.gridOptions.api.refreshCells();
  }

  onRowClicked(event) {
    console.log(event.data);
    const editMenuRef = this.dialog.open(EditItemComponent , {
      width: '30%',
      data: event.data
    });

    editMenuRef.afterClosed().subscribe((result) => {
      if( result !== 'DELETE') {
        this.itemService.editItem(result);
      }
    })
  }



}


export class itemRowData {
  id: number;
  name: string;
  unit: string;
  price: number;
  priceModel: string;
  itemCode: string;
  itemNo: string;
}