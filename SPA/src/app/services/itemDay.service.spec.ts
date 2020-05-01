/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ItemDayService } from './itemDay.service';

describe('Service: ItemDay', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ItemDayService]
    });
  });

  it('should ...', inject([ItemDayService], (service: ItemDayService) => {
    expect(service).toBeTruthy();
  }));
});
