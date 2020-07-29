/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PostTileComponent } from './post-tile.component';

describe('PostTileComponent', () => {
  let component: PostTileComponent;
  let fixture: ComponentFixture<PostTileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostTileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
