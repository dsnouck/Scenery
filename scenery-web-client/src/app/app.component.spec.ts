import { AppComponent } from "./app.component";

describe('AppComponent', () => {
  let systemUnderTest: AppComponent;

  beforeEach(() => {
    systemUnderTest = new AppComponent();
  });

  it('should be created', () => {
    // Act.
    expect(systemUnderTest).toBeTruthy();
  });
});
