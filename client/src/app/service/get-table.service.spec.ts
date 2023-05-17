import { TestBed } from '@angular/core/testing';

import { GetTableService } from './get-table.service';

describe('GetTableService', () => {
  let service: GetTableService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetTableService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
