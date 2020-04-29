import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { PriceCalendarService } from 'src/app/services/priceCalendar.service';
import { ItemPriceAndCurrencyResponse } from 'src/app/_models/ItemPriceAndCurrencyResponse.model';
import { Group } from 'src/app/_models/Group.model';
import { Item } from 'src/app/_models/Item.model';
import { ItemDay } from 'src/app/_models/ItemDay.model';
import * as moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports


import { AgGridAngular } from 'ag-grid-angular';
import { GridOptions } from 'ag-grid-community';
import { FormControl, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-price-calendar',
  templateUrl: './price-calendar.component.html',
  styleUrls: ['./price-calendar.component.scss']
})
export class PriceCalendarComponent implements OnInit {

  constructor(private priceCalendarService: PriceCalendarService) { }

  priceCalendar: ItemPriceAndCurrencyResponse[];

  private gridApi;
  private gridColumnApi;

  form;
  defaultColDef;
  days: Date[] = [];
  rowClassRules;
  firstRun = true;
  columnDefs;
  rowData: rowData[];
  gridOptions: GridOptions;

  // This method when the component is started
  ngOnInit() {
    const InitialDateEndDate = this.setDefaultDateRangeInForm();
    this.setInitialForm(InitialDateEndDate);
  }

  setInitialForm(date: Date) {
    this.form = new FormGroup({
      from: new FormControl(new Date()),
      to: new FormControl(date)
    });
  }

  setDefaultDateRangeInForm() {
    const InitalDateRange = new Date();
    InitalDateRange.setDate(InitalDateRange.getDate() + 3);
    return InitalDateRange;
  }



  OnGetDateRange( ) {
    this.priceCalendar = [];
    this.priceCalendarService.getPriceCalendarInInterval(this.form.value).subscribe((e: any) => {
      e.data.forEach(element => {
        this.priceCalendar.push(element);
      });
      console.log(this.priceCalendar);
      this.setData();
      this.defaultColDef = {
        editable: true,
      };
    });
  }


  submitGrid(){
    const ItemPriceAndCurrencyCommand = new ItemPriceAndCurrencyResponse();
    //ItemPriceAndCurrencyCommand.currency = this.priceCalendar.currency;

    this.gridOptions.rowData.forEach((row, index) => {
      var newGroup = new Group();
      if (!ItemPriceAndCurrencyCommand.groups.some(e => e.id === row.GroupID) ) {
        newGroup.id = row.GroupID;
        ItemPriceAndCurrencyCommand.groups.push(newGroup);
      }

      if (ItemPriceAndCurrencyCommand.groups.length === 0) {
        newGroup.id = row.GroupID;
        ItemPriceAndCurrencyCommand.groups.push(newGroup);
      };
    });
    this.gridOptions.rowData.forEach((row, index) => {
      var group = ItemPriceAndCurrencyCommand.groups.find(e => e.id === row.GroupID);
      var keys = Object.keys(row);
      var item = new Item();
      for(var i = 4; i < keys.length; i++) {
        var itemDay = new ItemDay();
        itemDay.date = new Date(moment(keys[i], 'DD-MM-YYYY' ).toString());
        itemDay.price = row[keys[i]];
        item.itemDays.push(itemDay);
      }

      item.name = row.Navn;
      item.id = row.id;
      item.price = row.Pris;

      group.items.push(item);
    });

    console.log(ItemPriceAndCurrencyCommand);
    
  }


  setData() {
    /*
    this.rowClassRules = {
      'red': (params) => {
        var date = moment('2020-04-17T10:11:56.985935+02:00').format('DD-MM-YYYY').toString();
      var changedPrice = params.data[date];
      var originalPrice = params.data.Pris;
      console.log(date);
      console.log(changedPrice);
      console.log(originalPrice);
      console.log(changedPrice != originalPrice);
      return changedPrice != originalPrice;
      }
     
    };
     */
    this.columnDefs = [
      {field: 'id' },
      {field: 'Navn' },
      {field: 'Pris'}
  ];

  var now = new Date(this.form.value.to);

  var daysOfYear = [];
    for (var d = new Date(this.form.value.from); d <= now; d.setDate(d.getDate() + 1)) {
    var date = moment(d).format('DD-MM-YYYY');
    daysOfYear.push(date);
  }

    daysOfYear.forEach(date => {
    this.columnDefs.push({field: date.toString() , cellStyle: (params) => {
    

      if (params.data.Pris !== params.data[date] ) {
        return {'font-weight': '800'}
      } else {
        return {'font-weight': '400'}
      }
  } });
  });

    this.rowData = [];
    this.priceCalendar.forEach(element => {
    element.groups.forEach((group: Group) => {
      this.columnDefs.push({field: 'GroupID', hide: true})
      group.items.forEach((item: Item) => {
        let element = new rowData();
        element['GroupID'] = group.id;
        element = { id: 1 , Navn: item.name, Pris: item.price , GroupID: group.id};
        item.itemDays.forEach((itemDay: ItemDay) => {
          element.id = itemDay.id;
          var date = moment(itemDay.date).format('DD-MM-YYYY');
          var dateString = date.toString();
          this.columnDefs.forEach(column => {
            if(column.field === dateString) {
              element[dateString] = itemDay.price;
            }
          });
        });
        this.firstRun = false;
        daysOfYear.forEach(el => {
          if(element[el] === undefined) {
            element[el] = item.price;
          };
        });
        this.rowData.push(element);
      });
    });
  });
  }


  // This method is invoked when the ag-grid is ready
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridOptions = this.gridColumnApi.columnController.gridOptionsWrapper.gridOptions;
    this.gridOptions.api.refreshCells();
  }

}

export class rowData {
  id: number;
  Navn: string;
  Pris: number;
  GroupID: number;
}


