import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaMenuComponent } from './sa-menu.component';

describe('SaMenuComponent', () => {
  let component: SaMenuComponent;
  let fixture: ComponentFixture<SaMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaMenuComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SaMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
