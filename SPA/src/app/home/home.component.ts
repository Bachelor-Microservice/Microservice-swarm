import { Component, OnInit, ApplicationRef, ChangeDetectionStrategy, NgZone } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent implements OnInit {

  isReadyToShowLogin = false;
  showSpinner = false;

  isAuthenticated: Observable<boolean>;
  isDoneLoading: Observable<boolean>;
  canActivateProtectedRoutes: Observable<boolean>;
  isDone: Observable<boolean>;
  testEmitter$ = new BehaviorSubject<boolean>(this.showSpinner);

  constructor(private authService: AuthService , private router: Router, private ref: ChangeDetectorRef , private ngZone: NgZone) {
    this.isAuthenticated = this.authService.isAuthenticated$;
    this.isDoneLoading = this.authService.isDoneLoading$;
    this.canActivateProtectedRoutes = this.authService.canActivateProtectedRoutes$;
    this.isDone = this.authService.isDone$;
    this.authService.runInitialLoginSequence();
  }

  ngOnInit() {
    this.isReadyToShowLogin = false;
    this.authService.isAuthenticated$.subscribe( e=> {
      this.isReadyToShowLogin = false;
      if (e === true) {
        this.router.navigate(['app/pricecalendar']);
      } else {
        this.isReadyToShowLogin = true;
      }
    });
  }

  OnLogin() {
    this.authService.login();
  }

  hideSpinner() {
    console.log("ikfnof");
    this.ref.markForCheck();
  }

}
