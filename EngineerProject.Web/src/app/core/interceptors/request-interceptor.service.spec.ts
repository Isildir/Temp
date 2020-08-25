import { TestBed, inject } from '@angular/core/testing';
import { RequestInterceptor } from './request-interceptor.service';

describe('Service: JwtInterceptor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RequestInterceptor]
    });
  });

  it('should ...', inject([RequestInterceptor], (service: RequestInterceptor) => {
    expect(service).toBeTruthy();
  }));
});
