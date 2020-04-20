/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PriceCalendarService } from './priceCalendar.service';

describe('Service: PriceCalendar', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PriceCalendarService]
    });
  });

  it('should ...', inject([PriceCalendarService], (service: PriceCalendarService) => {
    expect(service).toBeTruthy();
  }));
});
