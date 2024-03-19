import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginSaComponent } from './login-sa.component';

describe('LoginSaComponent', () => {
  let component: LoginSaComponent;
  let fixture: ComponentFixture<LoginSaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginSaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoginSaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
