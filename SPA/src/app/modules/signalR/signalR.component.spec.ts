/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SignalRComponent } from './signalR.component';

describe('SignalRComponent', () => {
  let component: SignalRComponent;
  let fixture: ComponentFixture<SignalRComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SignalRComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SignalRComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
