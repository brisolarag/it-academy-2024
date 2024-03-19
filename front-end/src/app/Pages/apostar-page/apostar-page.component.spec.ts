import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApostarPageComponent } from './apostar-page.component';

describe('ApostarPageComponent', () => {
  let component: ApostarPageComponent;
  let fixture: ComponentFixture<ApostarPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApostarPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApostarPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
