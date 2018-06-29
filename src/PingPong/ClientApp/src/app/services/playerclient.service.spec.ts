import { TestBed, inject } from '@angular/core/testing';

import { PlayerClientService } from './playerclient.service';

describe('PlayerclientService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PlayerClientService]
    });
  });

  it('should be created', inject([PlayerClientService], (service: PlayerClientService) => {
    expect(service).toBeTruthy();
  }));
});
