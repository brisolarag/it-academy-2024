import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarApostasPageComponent } from './listar-apostas-page.component';

describe('ListarApostasPageComponent', () => {
  let component: ListarApostasPageComponent;
  let fixture: ComponentFixture<ListarApostasPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListarApostasPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListarApostasPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
