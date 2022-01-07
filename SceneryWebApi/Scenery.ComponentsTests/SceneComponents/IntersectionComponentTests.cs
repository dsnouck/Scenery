// <copyright file="IntersectionComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponents;

using FluentAssertions;
using Moq;
using Scenery.Components.Implementations.SceneComponents;
using Scenery.Components.Interfaces;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;
using Xunit;

/// <summary>
/// Provides tests for <see cref="IntersectionComponent"/>.
/// </summary>
public class IntersectionComponentTests
{
    private readonly IntersectionComponent systemUnderTest;
    private readonly Mock<ILine3Component> line3ComponentTestDouble;
    private readonly Mock<ISceneComponent> sceneComponentTestDouble;
    private readonly Mock<ISceneComponent> otherSceneComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="IntersectionComponentTests"/> class.
    /// </summary>
    public IntersectionComponentTests()
    {
        this.line3ComponentTestDouble = new Mock<ILine3Component>();
        this.sceneComponentTestDouble = new Mock<ISceneComponent>();
        this.otherSceneComponentTestDouble = new Mock<ISceneComponent>();
        this.systemUnderTest = new IntersectionComponent(
            this.line3ComponentTestDouble.Object,
            this.sceneComponentTestDouble.Object,
            this.otherSceneComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="IntersectionComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheFirstSceneDoesNotContainThePointWhenContainsIsCalledThenFalseIsReturned()
    {
        // Arrange.
        var point = new Vector3();
        this.sceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(false);

        // Act.
        var result = this.systemUnderTest.Contains(point);

        // Assert.
        result.Should().BeFalse();
        this.sceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="IntersectionComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenOnlyTheFirstSceneContainsThePointWhenContainsIsCalledThenFalseIsReturned()
    {
        // Arrange.
        var point = new Vector3();
        this.sceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(true);
        this.otherSceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(false);

        // Act.
        var result = this.systemUnderTest.Contains(point);

        // Assert.
        result.Should().BeFalse();
        this.sceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        this.otherSceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="IntersectionComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenBothScenesContainThePointWhenContainsIsCalledThenTrueIsReturned()
    {
        // Arrange.
        var point = new Vector3();
        this.sceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(true);
        this.otherSceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(true);

        // Act.
        var result = this.systemUnderTest.Contains(point);

        // Assert.
        result.Should().BeTrue();
        this.sceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        this.otherSceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="IntersectionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheLineOfSightIsNullWhenGetAllInterceptsIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        Line3 lineOfSight = null;

        // Act.
        Action action = () => this.systemUnderTest.GetAllIntercepts(lineOfSight);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="IntersectionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenBothScenesGiveNoInterceptsWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
    {
        // Arrange.
        var lineOfSight = new Line3();
        this.sceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(new List<Intercept>());
        this.otherSceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(new List<Intercept>());

        // Act.
        var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

        // Assert.
        result.Should().BeEmpty();
        this.sceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        this.otherSceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="IntersectionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheFirstSceneGivesAnInterceptWhichIsNotContainedByTheSecondSceneWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
    {
        // Arrange.
        var lineOfSight = new Line3();
        this.sceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(new List<Intercept>
            {
                    new Intercept(),
            });
        this.otherSceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(new List<Intercept>());
        this.otherSceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(false);

        // Act.
        var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

        // Assert.
        result.Should().BeEmpty();
        this.line3ComponentTestDouble
            .Verify(component => component.GetPointAtDistance(It.IsAny<Line3>(), It.IsAny<double>()), Times.Once);
        this.sceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        this.otherSceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        this.otherSceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="IntersectionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheFirstSceneGivesAnInterceptWhichIsContainedByTheSecondSceneWhenGetAllInterceptsIsCalledThenOneIntercepteturned()
    {
        // Arrange.
        var lineOfSight = new Line3();
        this.sceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(new List<Intercept>
            {
                    new Intercept(),
            });
        this.otherSceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(new List<Intercept>());
        this.otherSceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(true);

        // Act.
        var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

        // Assert.
        result.Should().HaveCount(1);
        this.line3ComponentTestDouble
            .Verify(component => component.GetPointAtDistance(It.IsAny<Line3>(), It.IsAny<double>()), Times.Once);
        this.sceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        this.otherSceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        this.otherSceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="IntersectionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheSecondSceneGivesAnInterceptWhichIsNotContainedByTheFirstSceneWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
    {
        // Arrange.
        var lineOfSight = new Line3();
        this.sceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(new List<Intercept>());
        this.sceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(false);
        this.otherSceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(new List<Intercept>
            {
                    new Intercept(),
            });

        // Act.
        var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

        // Assert.
        result.Should().BeEmpty();
        this.line3ComponentTestDouble
            .Verify(component => component.GetPointAtDistance(It.IsAny<Line3>(), It.IsAny<double>()), Times.Once);
        this.sceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        this.sceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        this.otherSceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="IntersectionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheSecondSceneGivesAnInterceptWhichIsContainedByTheFirstSceneWhenGetAllInterceptsIsCalledThenOneInterceptIsReturned()
    {
        // Arrange.
        var lineOfSight = new Line3();
        this.sceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(new List<Intercept>());
        this.sceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(true);
        this.otherSceneComponentTestDouble
            .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
            .Returns(new List<Intercept>
            {
                    new Intercept(),
            });

        // Act.
        var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

        // Assert.
        result.Should().HaveCount(1);
        this.line3ComponentTestDouble
            .Verify(component => component.GetPointAtDistance(It.IsAny<Line3>(), It.IsAny<double>()), Times.Once);
        this.sceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        this.sceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        this.otherSceneComponentTestDouble
            .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
    }
}
