/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { HttpRequestWrapperService } from './http-request-wrapper.service';

describe('Service: ApiServiceHandler', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpRequestWrapperService]
    });
  });

  it('should ...', inject([HttpRequestWrapperService], (service: HttpRequestWrapperService) => {
    expect(service).toBeTruthy();
  }));
});
