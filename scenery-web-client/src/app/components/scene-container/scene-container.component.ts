import { Component, OnInit } from "@angular/core";
import { SceneContainerService } from "src/app/services/scene-container.service";

@Component({
  templateUrl: './scene-container.component.html',
  styleUrls: ['./scene-container.component.scss']
})
export class SceneContainerComponent implements OnInit {
  examples: string = '';
  
  constructor(private sceneContainerService: SceneContainerService) { }

  ngOnInit(): void {
    this.sceneContainerService.getSceneContainers().subscribe({
      next: examples => {
        this.examples = JSON.stringify(examples, null, 2);
      }
    });
  }
}
