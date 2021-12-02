import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CintaListComponent } from './cinta-list.component';

describe('CintaListComponent', () => {
  let component: CintaListComponent;
  let fixture: ComponentFixture<CintaListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CintaListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CintaListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
