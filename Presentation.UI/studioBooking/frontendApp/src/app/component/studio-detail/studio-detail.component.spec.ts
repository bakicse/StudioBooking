import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudioDetailComponent } from './studio-detail.component';

describe('StudioDetailComponent', () => {
  let component: StudioDetailComponent;
  let fixture: ComponentFixture<StudioDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StudioDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudioDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
