/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AppstartComponent } from './appstart.component';

describe('AppstartComponent', () => {
  let component: AppstartComponent;
  let fixture: ComponentFixture<AppstartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppstartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppstartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
