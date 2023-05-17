import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoggatiComponent } from './loggati.component';

describe('LoggatiComponent', () => {
  let component: LoggatiComponent;
  let fixture: ComponentFixture<LoggatiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoggatiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoggatiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
