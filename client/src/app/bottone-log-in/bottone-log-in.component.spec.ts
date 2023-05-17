import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BottoneLogInComponent } from './bottone-log-in.component';

describe('BottoneLogInComponent', () => {
  let component: BottoneLogInComponent;
  let fixture: ComponentFixture<BottoneLogInComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BottoneLogInComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BottoneLogInComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
