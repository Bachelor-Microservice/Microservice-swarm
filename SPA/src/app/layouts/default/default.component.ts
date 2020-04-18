import { Component, OnInit } from '@angular/core';
import { SidenavService } from 'src/app/services/sidenav.service';
import { onMainContentChange, onSideNavChange } from '../../shared/animations/animations';


@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.scss'],
  animations: [onSideNavChange , onMainContentChange]
})
export class DefaultComponent implements OnInit {

  public sideNav: boolean = true;



  constructor(private _sidenavService: SidenavService ) {
    this._sidenavService.sideNavState$.subscribe(res => {
      console.log(res);
      this.sideNav = res;
    })
   }

  ngOnInit(): void {
  }


 


}
