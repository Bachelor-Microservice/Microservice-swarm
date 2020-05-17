import { Component, OnInit, Output , EventEmitter } from '@angular/core';
import { SidenavService } from 'src/app/services/sidenav.service';
import { onSideNavChange, animateText } from '../../animations/animations';

import { OAuthService } from 'angular-oauth2-oidc';
import { AuthService } from 'src/app/services/auth.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { HubconnectorService } from 'src/app/services/hubconnector.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  animations: [onSideNavChange, animateText]
})
export class HeaderComponent implements OnInit {


  @Output() toggleSidebarEvent: EventEmitter<any> = new EventEmitter();

  public sideNavState: boolean = true;
  public linkText: boolean = true;
  public numbers = 2;
  public excelFiles: any[];

  constructor(private _sidenavService: SidenavService, private authService: AuthService , private router: Router , private hubService: HubconnectorService) {
   }

  ngOnInit(): void {
    this.excelFiles = [];
    this.hubService.OpenConnection();
    this.hubService.ExcelEventRecieved$.subscribe( data => {
      var dec = window.atob(data);
      var myArr = new Uint8Array(dec.length)
      for(var i = 0; i < Object.keys(dec).length; i++){
          myArr[i] = dec.charCodeAt(i);
      }
      var blob = new Blob([myArr], {type: 'application/vnd.ms-excel'});
      this.excelFiles.push(blob);
    });
    this.excelFiles.pop();
  }

  toggleSidebar() {

    this.sideNavState = !this.sideNavState;
    setTimeout(() => {
      this.linkText = this.sideNavState;
    }, 200)
    this._sidenavService.sideNavState$.next(this.sideNavState);
  }


  OnLogout() {
    this.authService.logout();
  }

  downloadExcelfile(index ) {
   
    var fileURL = URL.createObjectURL(this.excelFiles[index]);
    let a = document.createElement("a");
    document.body.appendChild(a);
    a.style.display = "none";
    a.href = fileURL;
    a.target = "_blank";
    a.download = "Pricecalendar.xlsx";
    a.click();
    a.remove();
    this.excelFiles.splice(index , 1);
  }

}
