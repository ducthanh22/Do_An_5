import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProduceComponent } from './produce.component';

describe('ProduceComponent', () => {
  let component: ProduceComponent;
  let fixture: ComponentFixture<ProduceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProduceComponent]
    });
    fixture = TestBed.createComponent(ProduceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
