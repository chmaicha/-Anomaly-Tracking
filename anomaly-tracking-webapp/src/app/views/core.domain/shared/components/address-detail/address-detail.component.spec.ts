import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AddressDetailComponent } from './address-detail.component';

describe('AddressDetailComponent', () => {
  let component: AddressDetailComponent;
  let fixture: ComponentFixture<AddressDetailComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AddressDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
