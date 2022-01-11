// <copyright file="ColoredComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponents;

using FluentAssertions;
using Moq;
using Scenery.Components.Implementations.SceneComponents;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;
using Xunit;

/// <summary>
/// Provides tests for <see cref="ColoredComponent"/>.
/// </summary>
public class ColoredComponentTests
{
    private readonly ColoredComponent systemUnderTest;
    private readonly Mock<ISceneComponent> sceneComponentTestDouble;
    private readonly Color color;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColoredComponentTests"/> class.
    /// </summary>
    public ColoredComponentTests()
    {
        this.sceneComponentTestDouble = new Mock<ISceneComponent>();
        this.color = new Color();
        this.systemUnderTest = new ColoredComponent(
            this.sceneComponentTestDouble.Object,
            this.color);
    }

    /// <summary>
    /// Tests <see cref="ColoredComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenAPointWhenContainsIsCalledThenSceneComponentContainsIsCalled()
    {
        // Arrange.
        var point = new Vector3();
        this.sceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(true);

        // Act.
        var result = this.systemUnderTest.Contains(point);

        // Assert.
        result.Should().BeTrue();
        this.sceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="ColoredComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheSceneGivesNoInterceptsWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
    {
        // Arrange.
        var lineOfSight = new Line3();
        var originalIntercepts = new List<Intercept>();
        this.sceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(originalIntercepts);

        // Act.
        var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

        // Assert.
        result.Should().BeEmpty();
        this.sceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="ColoredComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheSceneGivesOneInterceptWhenGetAllInterceptsIsCalledThenOneInterceptIsReturned()
    {
        // Arrange.
        var lineOfSight = new Line3();
        var originalIntercepts = new List<Intercept>
            {
                new Intercept(),
            };
        this.sceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(originalIntercepts);

        // Act.
        var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

        // Assert.
        result.Should().HaveCount(1);
        this.sceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
    }
}
