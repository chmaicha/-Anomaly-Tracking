import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MediaDownloadComponent } from './media-download.component';

describe('FileDownloadComponent', () => {
  let component: MediaDownloadComponent;
  let fixture: ComponentFixture<MediaDownloadComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MediaDownloadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MediaDownloadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
