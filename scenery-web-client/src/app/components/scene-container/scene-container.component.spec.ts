// TODO: Use single quotes everywhere. Use prettier?
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { of } from "rxjs";
import { SceneContainerService } from "src/app/services/scene-container.service";
import { SceneContainerComponent } from "./scene-container.component";

// TODO: Use a better testing framework.
describe('SceneContainerComponent', () => {
  let component: SceneContainerComponent;
  let fixture: ComponentFixture<SceneContainerComponent>;
  let sceneContainerServiceSpy: jasmine.SpyObj<SceneContainerService>;

  beforeEach(async () => {
    sceneContainerServiceSpy = jasmine.createSpyObj<SceneContainerService>('SceneContainerService', ['getSceneContainers']);
    sceneContainerServiceSpy.getSceneContainers.and.returnValue(of(null));

    await TestBed.configureTestingModule({
      providers: [{ provide: SceneContainerService, useValue: sceneContainerServiceSpy }],
      declarations: [SceneContainerComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SceneContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
