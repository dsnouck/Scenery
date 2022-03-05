import { HttpClient } from "@angular/common/http";
import { TestBed } from "@angular/core/testing";
import { SceneContainerService } from "./scene-container.service";

describe('SceneContainerService', () => {
  let service: SceneContainerService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj<HttpClient>('HttpClient', ['get']);

    TestBed.configureTestingModule({
      providers: [{ provide: HttpClient, useValue: httpClientSpy }]
    });

    service = TestBed.inject(SceneContainerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
