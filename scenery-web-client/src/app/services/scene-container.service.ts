import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SceneContainerService {
  constructor(private httpClient: HttpClient) { }

  getSceneContainers(): Observable<any> {
    return this.httpClient.get<any>('https://localhost:44398/SceneContainers')
  }
}
