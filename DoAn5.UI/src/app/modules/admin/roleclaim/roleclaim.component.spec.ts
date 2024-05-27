import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleclaimComponent } from './roleclaim.component';

describe('RoleclaimComponent', () => {
  let component: RoleclaimComponent;
  let fixture: ComponentFixture<RoleclaimComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RoleclaimComponent]
    });
    fixture = TestBed.createComponent(RoleclaimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
