// <copyright file="SceneContainerComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests;

using FluentAssertions;
using Moq;
using Scenery.Components.Implementations;
using Scenery.Components.Interfaces;
using Scenery.Models;
using Scenery.Models.Scenes;
using Xunit;

/// <summary>
/// Provides tests for <see cref="SceneContainerComponent"/>.
/// </summary>
public class SceneContainerComponentTests
{
    private readonly SceneContainerComponent systemUnderTest;
    private readonly Mock<IBitmapFileComponent> bitmapFileComponentTestDouble;
    private readonly Mock<IProjectorComponent> projectorComponentTestDouble;
    private readonly Mock<ISamplerComponent> samplerComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneContainerComponentTests"/> class.
    /// </summary>
    public SceneContainerComponentTests()
    {
        this.bitmapFileComponentTestDouble = new Mock<IBitmapFileComponent>();
        this.projectorComponentTestDouble = new Mock<IProjectorComponent>();
        this.samplerComponentTestDouble = new Mock<ISamplerComponent>();
        this.systemUnderTest = new SceneContainerComponent(
            this.bitmapFileComponentTestDouble.Object,
            this.projectorComponentTestDouble.Object,
            this.samplerComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="SceneContainerComponent.GetExamples"/>.
    /// </summary>
    [Fact]
    public void WhenGetExamplesIsCalledThenTheResultIsNotNull()
    {
        // Arrange.

        // Act.
        var result = this.systemUnderTest.GetExamples();

        // Assert.
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests <see cref="SceneContainerComponent.GetStream(SceneContainer)"/>.
    /// </summary>
    [Fact]
    public void GivenASceneContainerWhenGetStreamIsCalledThenTheCorrectActionsArePerformed()
    {
        // Arrange.
        var sceneContainer = new SceneContainer();

        // Act.
        this.systemUnderTest.GetStream(sceneContainer);

        // Assert.
        this.projectorComponentTestDouble
            .Verify(component => component.ProjectSceneToImage(It.IsAny<Scene>(), It.IsAny<ProjectorSettings>()), Times.Once);
        this.samplerComponentTestDouble
            .Verify(component => component.SampleImageToBitmap(It.IsAny<Func<Vector2, Color>>(), It.IsAny<SamplerSettings>()), Times.Once);
        this.bitmapFileComponentTestDouble
            .Verify(component => component.GetStream(It.IsAny<List<List<Color>>>()), Times.Once);
    }
}
