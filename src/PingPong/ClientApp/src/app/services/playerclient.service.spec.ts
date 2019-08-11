import { TestBed, inject } from '@angular/core/testing';

import { PlayerClient } from './playerclient.service';

describe('PlayerClient', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PlayerClient]
    });
  });

  it('should be created', inject([PlayerClient], (service: PlayerClient) => {
    expect(service).toBeTruthy();
  }));
});
