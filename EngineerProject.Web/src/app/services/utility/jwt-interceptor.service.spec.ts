import { TestBed, inject } from '@angular/core/testing';
import { JwtInterceptor } from './jwt-interceptor.service';

describe('Service: JwtInterceptor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [JwtInterceptor]
    });
  });

  it('should ...', inject([JwtInterceptor], (service: JwtInterceptor) => {
    expect(service).toBeTruthy();
  }));
});
