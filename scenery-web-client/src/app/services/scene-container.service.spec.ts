import { SceneContainerService } from "./scene-container.service";
import { HttpClient } from "@angular/common/http";
import { mock, instance } from "ts-mockito";

describe('SceneContainerService', () => {
  let systemUnderTest: SceneContainerService;
  let httpClientTestDouble: HttpClient;

  beforeEach(() => {
    httpClientTestDouble = mock(HttpClient);
    systemUnderTest = new SceneContainerService(instance(httpClientTestDouble));
  });

  it('should be created', () => {
    // Assert.
    expect(systemUnderTest).toBeTruthy();
  });
});
