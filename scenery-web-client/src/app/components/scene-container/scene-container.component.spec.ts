// TODO: Use single quotes everywhere. Use prettier?
import { SceneContainerComponent } from "./scene-container.component";
import { SceneContainerService } from "src/app/services/scene-container.service";
import { mock, instance } from "ts-mockito";

// TODO: Add unit tests.
describe('SceneContainerComponent', () => {
  let systemUnderTest: SceneContainerComponent;
  let sceneContainerServiceTestDouble: SceneContainerService;

  beforeEach(() => {
    sceneContainerServiceTestDouble = mock(SceneContainerService);
    systemUnderTest = new SceneContainerComponent(instance(sceneContainerServiceTestDouble));
  });

  it('should be created', () => {
    // Assert.
    expect(systemUnderTest).toBeTruthy();
  });
});
