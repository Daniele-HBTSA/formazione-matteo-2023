import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BottoneRegisterComponent } from './bottone-register.component';

describe('BottoneRegisterComponent', () => {
  let component: BottoneRegisterComponent;
  let fixture: ComponentFixture<BottoneRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BottoneRegisterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BottoneRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
