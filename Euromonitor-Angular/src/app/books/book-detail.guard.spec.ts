import { TestBed } from '@angular/core/testing';

import { BookDetailGuard } from './book-detail.guard';

describe('BookDetailGuard', () => {
  let guard: BookDetailGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(BookDetailGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
