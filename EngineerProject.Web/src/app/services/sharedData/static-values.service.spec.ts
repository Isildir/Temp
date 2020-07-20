/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { StaticValuesService } from './static-values.service';

describe('Service: StaticValues', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StaticValuesService]
    });
  });

  it('should ...', inject([StaticValuesService], (service: StaticValuesService) => {
    expect(service).toBeTruthy();
  }));
});
