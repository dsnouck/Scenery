// <copyright file="UnionComponentTests.cs" company="dsnouck">
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
/// Provides tests for <see cref="UnionComponent"/>.
/// </summary>
public class UnionComponentTests
{
    private readonly UnionComponent systemUnderTest;
    private readonly Mock<ILine3Component> line3ComponentTestDouble;
    private readonly Mock<ISceneComponent> sceneComponentTestDouble;
    private readonly Mock<ISceneComponent> otherSceneComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnionComponentTests"/> class.
    /// </summary>
    public UnionComponentTests()
    {
        this.line3ComponentTestDouble = new Mock<ILine3Component>();
        this.sceneComponentTestDouble = new Mock<ISceneComponent>();
        this.otherSceneComponentTestDouble = new Mock<ISceneComponent>();
        this.systemUnderTest = new UnionComponent(
            this.line3ComponentTestDouble.Object,
            this.sceneComponentTestDouble.Object,
            this.otherSceneComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="UnionComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheFirstSceneContainsThePointWhenContainsIsCalledThenTrueIsReturned()
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
    /// Tests <see cref="UnionComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenBothScenesDoNotContainThePointWhenContainsIsCalledThenFalseIsReturned()
    {
        // Arrange.
        var point = new Vector3();
        this.sceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(false);
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
    /// Tests <see cref="UnionComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenOnlyTheSecondSceneContainsThePointWhenContainsIsCalledThenTrueIsReturned()
    {
        // Arrange.
        var point = new Vector3();
        this.sceneComponentTestDouble
            .Setup(component => component.Contains(It.IsAny<Vector3>()))
            .Returns(false);
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
    /// Tests <see cref="UnionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheLineOfSightIsNullWhenGetAllInterceptsIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var lineOfSight = default(Line3);

        // Act.
        var action = () => this.systemUnderTest.GetAllIntercepts(lineOfSight);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="UnionComponent.GetAllIntercepts(Line3)"/>.
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
    /// Tests <see cref="UnionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheFirstSceneGivesAnInterceptWhichIsNotContainedByTheSecondSceneWhenGetAllInterceptsIsCalledThenOneInterceptIsReturned()
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
    /// Tests <see cref="UnionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheFirstSceneGivesAnInterceptWhichIsContainedByTheSecondSceneWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
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
    /// Tests <see cref="UnionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheSecondSceneGivesAnInterceptWhichIsNotContainedByTheFirstSceneWhenGetAllInterceptsIsCalledThenOneInterceptIsReturned()
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

    /// <summary>
    /// Tests <see cref="UnionComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheSecondSceneGivesAnInterceptWhichIsContainedByTheFirstSceneWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
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
}
