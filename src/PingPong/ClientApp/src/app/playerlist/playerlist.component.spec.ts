import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PlayerlistComponent } from './playerlist.component';

describe('PlayerlistComponent', () => {
  let component: PlayerlistComponent;
  let fixture: ComponentFixture<PlayerlistComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
