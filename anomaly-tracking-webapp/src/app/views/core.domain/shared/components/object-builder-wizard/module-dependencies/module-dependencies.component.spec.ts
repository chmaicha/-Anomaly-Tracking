import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ModuleDependenciesComponent } from './module-dependencies.component';

describe('ModuleDependenciesComponent', () => {
  let component: ModuleDependenciesComponent;
  let fixture: ComponentFixture<ModuleDependenciesComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ModuleDependenciesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModuleDependenciesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
