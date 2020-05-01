import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { PriceCalendarService } from 'src/app/services/priceCalendar.service';
import { ItemPriceAndCurrencyResponse } from 'src/app/_models/ItemPriceAndCurrencyResponse.model';
import { Group } from 'src/app/_models/Group.model';
import { Item } from 'src/app/_models/Item.model';
import { ItemDay } from 'src/app/_models/ItemDay.model';
import * as moment from 'moment';
import { GridOptions } from 'ag-grid-community';
import { FormControl, FormGroup } from '@angular/forms';
import { ItemDayDTO } from 'src/app/_models/ItemDayDTO.model';
import { ItemDayService } from 'src/app/services/itemDay.service';

@Component({
  selector: 'app-price-calendar',
  templateUrl: './price-calendar.component.html',
  styleUrls: ['./price-calendar.component.scss']
})
export class PriceCalendarComponent implements OnInit {

  constructor(private priceCalendarService: PriceCalendarService , private itemDayService: ItemDayService) { }

  priceCalendar: ItemPriceAndCurrencyResponse[];

  ItemDayDTO: ItemDayDTO[] = [];

  form;
  defaultColDef;
  days: Date[] = [];
  rowClassRules;
  firstRun = true;
  columnDefs;
  rowData: rowData[];

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
    this.priceCalendarService.getPriceCalendarInInterval(this.form.value).subscribe((e: any) => {
      this.priceCalendar = [];
      e.data.forEach(element => {
        this.priceCalendar.push(element);
      });
      this.setData();
    });
  }

  onCellValueChanged(event) {
    var itemDay = new ItemDayDTO();
    itemDay.date =  moment( event.colDef.field , "DD-MM-YYYY").toDate();
    itemDay.id =  this.getRandomInt();
    itemDay.price = +event.data[event.colDef.field];

    this.priceCalendar.forEach( element => {
      element.groups.forEach( e=> {
        e.items.forEach(item => {
          item.itemDays.forEach(itemday => {
            var originalItemDay = new Date(itemday.date);
            console.log(originalItemDay);
            if (originalItemDay.getTime() === itemDay.date.getTime()) {
             itemDay.id = itemday.id;
           }
          });
          itemDay.itemId = item.id;
        });
      });
    })
    if (this.ItemDayDTO.some(e => e.date.getTime() === itemDay.date.getTime() && e.id === itemDay.id) ) {
      var itemIndex = this.ItemDayDTO.findIndex(e => e.date.getTime() === itemDay.date.getTime() && e.id === itemDay.id);
      this.ItemDayDTO[itemIndex].price = itemDay.price;
    }else{
      this.ItemDayDTO.push(itemDay);
    }
  }
  submitGrid(){
   console.log(this.ItemDayDTO);
   let itemDayListDTO = {ItemDays: this.ItemDayDTO};
   this.itemDayService.AddItemDays(itemDayListDTO); // remeber to subscribe
  }


  setData() {
    this.columnDefs = [
      {field: 'id' },
      {field: 'Navn' },
      {field: 'Pris'}
  ];
    const daysOfYear = this.MakeDatesFromRangeChoosen();

    this.AddDateToColumnHeader(daysOfYear);


    this.rowData = [];
    this.priceCalendar.forEach(element => {
    element.groups.forEach((group: Group) => {
      this.columnDefs.push({field: 'GroupID', hide: true});
      group.items.forEach((item: Item) => {
        let element = new rowData();
        element['GroupID'] = group.id;
        element = { id: '' , Navn: item.name, Pris: item.price , GroupID: group.id};
        item.itemDays.forEach((itemDay: ItemDay) => {
          element.id = item.id;
          var date = moment(itemDay.date).format('DD-MM-YYYY');
          var dateString = date.toString();
          this.columnDefs.forEach(column => {
            if(column.field === dateString) {
              element[dateString] = itemDay.price;
              this.columnDefs.push({field: itemDay.id.toString() , hide: true });
              element[itemDay.id.toString()] = itemDay.id;
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

  MakeDatesFromRangeChoosen() {
    const dateRange = [];
    const endDateSelected = new Date(this.form.value.to);
    for (var d = new Date(this.form.value.from); d <= endDateSelected; d.setDate(d.getDate() + 1)) {
      var date = moment(d).format('DD-MM-YYYY');
      dateRange.push(date);
    }

    return dateRange;
  }

  AddDateToColumnHeader(daysOfYear) {
    daysOfYear.forEach(date => {
      this.columnDefs.push({field: date.toString(),  editable: true , cellStyle: (cell) => {
        return this.AddDynamicCellStyling(cell , date);
      }});
    });
  }

  AddDynamicCellStyling(cell, date) {
    console.log(cell.data[date]);
    if (cell.data.Pris !== +cell.data[date] ) {
      return {'font-weight': '800'}
    } else {
      return {'font-weight': '400'}
    }
  }

  getRandomInt() {
    return Math.floor(Math.random() * (100000 - 0 + 1)) + 0;
  }

}

export class rowData {
  id: string;
  Navn: string;
  Pris: number;
  GroupID: number;
}


