/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WindowScrollService } from './window-scroll.service';

describe('Service: WindowScroll', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WindowScrollService]
    });
  });

  it('should ...', inject([WindowScrollService], (service: WindowScrollService) => {
    expect(service).toBeTruthy();
  }));
});
