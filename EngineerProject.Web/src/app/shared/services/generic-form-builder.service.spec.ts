/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GenericFormBuilderService } from './generic-form-builder.service';

describe('Service: GenericFormBuilder', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GenericFormBuilderService]
    });
  });

  it('should ...', inject([GenericFormBuilderService], (service: GenericFormBuilderService) => {
    expect(service).toBeTruthy();
  }));
});
