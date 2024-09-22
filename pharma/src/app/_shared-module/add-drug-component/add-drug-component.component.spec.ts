import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDrugModalComponent } from './add-drug-component.component';

describe('AddDrugComponentComponent', () => {
  let component: AddDrugModalComponent;
  let fixture: ComponentFixture<AddDrugModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddDrugModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddDrugModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
