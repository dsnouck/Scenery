// <copyright file="SceneContainersControllerTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.ControllersTests.Controllers;

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Scenery.Components.Interfaces;
using Scenery.Controllers.Controllers;
using Scenery.Models;
using Xunit;

/// <summary>
/// Provides tests for <see cref="SceneContainersController"/>.
/// </summary>
public class SceneContainersControllerTests
{
    private readonly SceneContainersController systemUnderTest;
    private readonly Mock<ISceneContainerComponent> sceneContainerComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneContainersControllerTests"/> class.
    /// </summary>
    public SceneContainersControllerTests()
    {
        this.sceneContainerComponentTestDouble = new Mock<ISceneContainerComponent>();
        this.systemUnderTest = new SceneContainersController(
            this.sceneContainerComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="SceneContainersController.Get"/>.
    /// </summary>
    [Fact]
    public void WhenGetIsCalledThenExamplesAreReturned()
    {
        // Act.
        var result = this.systemUnderTest.Get();

        // Assert.
        result.Should().BeOfType<OkObjectResult>();
        this.sceneContainerComponentTestDouble
            .Verify(component => component.GetExamples(), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="SceneContainersController.Post(SceneContainer)"/>.
    /// </summary>
    [Fact]
    public void GivenASceneContainerWhenPostIsCalledThenSceneContainerComponentGetStreamIsCalled()
    {
        // Arrange.
        var sceneContainer = new SceneContainer();
        using var stream = new MemoryStream();
        this.sceneContainerComponentTestDouble
            .Setup(component => component.GetStream(It.IsAny<SceneContainer>()))
            .Returns(stream);

        // Act.
        var result = this.systemUnderTest.Post(sceneContainer);

        // Assert.
        result.Should().BeOfType<FileStreamResult>();
        this.sceneContainerComponentTestDouble
            .Verify(component => component.GetStream(It.IsAny<SceneContainer>()));
    }
}
