import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { PriceCalendarService } from 'src/app/services/priceCalendar.service';
import { ItemPriceAndCurrencyResponse } from 'src/app/_models/ItemPriceAndCurrencyResponse.model';
import { Group } from 'src/app/_models/Group.model';
import { Item } from 'src/app/_models/Item.model';
import { ItemDay } from 'src/app/_models/ItemDay.model';
import { FormControl, FormGroup } from '@angular/forms';
import { ItemDayDTO } from 'src/app/_models/ItemDayDTO.model';
import moment from 'moment';
import { ItemDayService } from 'src/app/services/itemDay.service';
import { NotificationsService } from 'angular2-notifications';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { MatDialog } from '@angular/material/dialog';
import { ExcelDownloadComponent } from './ExcelDownload/ExcelDownload.component';


@Component({
  selector: 'app-price-calendar',
  templateUrl: './price-calendar.component.html',
  styleUrls: ['./price-calendar.component.scss']
})
export class PriceCalendarComponent implements OnInit {

  
  constructor(private priceCalendarService: PriceCalendarService , private itemDayService: ItemDayService, 
    private notifier: NotificationsService, public dialog: MatDialog) {
      
    
   }

  priceCalendar: ItemPriceAndCurrencyResponse[];

  ItemDayDTO: ItemDayDTO[] = [];

  form;
  defaultColDef;
  days: Date[] = [];
  rowClassRules;
  firstRun = true;
  columnDefs;
  rowData: rowData[];
  private _hubConnection: HubConnection;
  // This method when the component is started
  ngOnInit() {
     
    this._hubConnection = new HubConnectionBuilder().withUrl(environment.api + 'hub' )
    .build();

    this._hubConnection.on('Send', (data: any) => {
    const received = `Received: ${data}`;
    console.log(data);
    });

    this._hubConnection.on('HELLO' , data => {
      console.log("RESPONSE");
      
      var dec = window.atob(data);
      var myArr = new Uint8Array(dec.length)
      for(var i = 0; i < Object.keys(dec).length; i++){
          myArr[i] = dec.charCodeAt(i);
      }
      var blob = new Blob([myArr], {type: 'application/vnd.ms-excel'});
      const dialogRef = this.dialog.open(ExcelDownloadComponent, {
        width: '250px',
        data: {data: blob}
      });
    });

    this._hubConnection.start()
    .then(() => {
      console.log('Hub connection started');
    })
    .catch(err => {
    console.log('Error while establishing connection');
    });
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

  OnExportToExcel() {
    this.priceCalendarService.getExcel(this.form.value).subscribe( e=> {
      this.notifier.success('Excel export success' , 'Your file will be downloaded shortly' ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
    })
   
  }

  onCellValueChanged(event) {
    var itemDay = new ItemDayDTO();
    let date =  moment( event.colDef.field , "DD-MM-YYYY").toDate();
    itemDay.itemId = event.data.id;
    itemDay.id =  this.getRandomInt();
    itemDay.price = +event.data[event.colDef.field];

    this.priceCalendar.forEach( element => {
      element.groups.forEach( e=> {
        e.items.forEach(item => {
          item.itemDays.forEach(itemday => {
            var originalItemDay = new Date(itemday.date);
            if (originalItemDay.getTime() === date.getTime() && itemday.id === itemDay.id) {
             itemDay.id = itemday.id;
           }
          });
          
        });
      });
    });
    if (this.ItemDayDTO.some(e => new Date(e.date).getTime() === date.getTime() && e.id === itemDay.id) ) {
      var itemIndex = this.ItemDayDTO.findIndex(e => new Date(e.date).getTime() === date.getTime() && e.id === itemDay.id);
      this.ItemDayDTO[itemIndex].price = itemDay.price;
    }else{
      this.ItemDayDTO.push(itemDay);
    }
    date.setHours(2);
    itemDay.date = date.toISOString();
    console.log(this.ItemDayDTO);
    
  }
  submitGrid(){
   let itemDayListDTO = {ItemDays: this.ItemDayDTO};
   console.log(itemDayListDTO);
   itemDayListDTO.ItemDays.forEach(itemDayDTO => {
     itemDayDTO.date
   })
   this.itemDayService.AddItemDays(itemDayListDTO).subscribe(e => {
     this.priceCalendarService.getPriceCalendarInInterval(this.form.value).subscribe((e: any) => {
      this.notifier.success('Updated Succesfully' , null ,{
        timeOut: 3000,
        showProgressBar: true,
        pauseOnHover: false,
        clickToClose: true
      });
      this.priceCalendar = [];
      e.data.forEach(element => {
        this.priceCalendar.push(element);
      });
      this.setData();
    });
   });
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
        element = { id: item.id , Navn: item.name, Pris: item.price , GroupID: group.id};
        item.itemDays.forEach((itemDay: ItemDay) => {
          console.log(item.id);
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


