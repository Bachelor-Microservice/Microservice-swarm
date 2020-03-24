import { Component, OnInit, Output , EventEmitter } from '@angular/core';
import { SidenavService } from 'src/app/services/sidenav.service';
import { onSideNavChange, animateText } from '../../animations/animations';


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

  constructor(private _sidenavService: SidenavService) { }

  ngOnInit(): void {
  }

  toggleSidebar() {

    this.sideNavState = !this.sideNavState;
    setTimeout(() => {
      this.linkText = this.sideNavState;
    }, 200)
    this._sidenavService.sideNavState$.next(this.sideNavState);
  }

}
