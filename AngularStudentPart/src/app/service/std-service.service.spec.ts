import { TestBed } from '@angular/core/testing';

import { StdServiceService } from './std-service.service';

describe('StdServiceService', () => {
  let service: StdServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StdServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
