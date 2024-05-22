import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportBillComponent } from './import-bill.component';

describe('ImportBillComponent', () => {
  let component: ImportBillComponent;
  let fixture: ComponentFixture<ImportBillComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImportBillComponent]
    });
    fixture = TestBed.createComponent(ImportBillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
