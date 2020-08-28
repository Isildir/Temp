/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ApiServiceHandlerService } from './api-service-handler.service';

describe('Service: ApiServiceHandler', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ApiServiceHandlerService]
    });
  });

  it('should ...', inject([ApiServiceHandlerService], (service: ApiServiceHandlerService) => {
    expect(service).toBeTruthy();
  }));
});
