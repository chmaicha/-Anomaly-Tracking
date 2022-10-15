import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingDialogueComponent } from './setting-dialogue.component';

describe('SettingDialogueComponent', () => {
  let component: SettingDialogueComponent;
  let fixture: ComponentFixture<SettingDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SettingDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
