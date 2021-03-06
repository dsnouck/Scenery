// <copyright file="AffinelyTransformedComponentTests.cs" company="dsnouck">
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
/// Provides tests for <see cref="AffinelyTransformedComponent"/>.
/// </summary>
public class AffinelyTransformedComponentTests
{
    private readonly AffinelyTransformedComponent systemUnderTest;
    private readonly Mock<IMatrix4Component> matrix4ComponentTestDouble;
    private readonly Mock<ISceneComponent> sceneComponentTestDouble;
    private readonly Matrix4 transformation;
    private readonly Matrix4 backwardTransformation;

    /// <summary>
    /// Initializes a new instance of the <see cref="AffinelyTransformedComponentTests"/> class.
    /// </summary>
    public AffinelyTransformedComponentTests()
    {
        this.matrix4ComponentTestDouble = new Mock<IMatrix4Component>();
        this.sceneComponentTestDouble = new Mock<ISceneComponent>();
        this.transformation = new Matrix4();
        this.backwardTransformation = new Matrix4();
        this.systemUnderTest = new AffinelyTransformedComponent(
            this.matrix4ComponentTestDouble.Object,
            this.sceneComponentTestDouble.Object,
            this.transformation,
            this.backwardTransformation);
    }

    /// <summary>
    /// Tests <see cref="AffinelyTransformedComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenThePointIsNullWhenContainsIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var point = default(Vector3);

        // Act.
        var action = () => this.systemUnderTest.Contains(point);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="AffinelyTransformedComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenAPointWhenContainsIsCalledThenTheCorrectCalculationsArePerformed()
    {
        // Arrange.
        var point = new Vector3();
        this.matrix4ComponentTestDouble
            .Setup(component => component.Multiply(It.IsAny<Matrix4>(), It.IsAny<Vector4>()))
            .Returns(new Vector4());

        // Act.
        var result = this.systemUnderTest.Contains(point);

        // Assert.
        this.matrix4ComponentTestDouble
            .Verify(component => component.Multiply(It.IsAny<Matrix4>(), It.IsAny<Vector4>()), Times.Once);
        this.sceneComponentTestDouble
            .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="AffinelyTransformedComponent.GetAllSurfaceIntersections(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheLineOfSightIsNullWhenGetAllSurfaceIntersectionsIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var lineOfSight = default(Line3);

        // Act.
        var action = () => this.systemUnderTest.GetAllSurfaceIntersections(lineOfSight);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="AffinelyTransformedComponent.GetAllSurfaceIntersections(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenALineOfSightWhenGetAllSurfaceIntersectionsIsCalledThenTheCorrectCalculationsArePerformed()
    {
        // Arrange.
        var lineOfSight = new Line3();
        this.matrix4ComponentTestDouble
            .Setup(component => component.Multiply(It.IsAny<Matrix4>(), It.IsAny<Vector4>()))
            .Returns(new Vector4());
        this.sceneComponentTestDouble
            .Setup(component => component.GetAllSurfaceIntersections(It.IsAny<Line3>()))
            .Returns(new List<SurfaceIntersection>
            {
                    new SurfaceIntersection(),
            });

        // Act.
        var result = this.systemUnderTest.GetAllSurfaceIntersections(lineOfSight);

        // Assert.
        result.Should().HaveCount(1);
        result.Single().Normal().Should().NotBeNull();
        this.sceneComponentTestDouble
            .Verify(component => component.GetAllSurfaceIntersections(It.IsAny<Line3>()), Times.Once);
        this.matrix4ComponentTestDouble
            .Verify(component => component.Multiply(It.IsAny<Matrix4>(), It.IsAny<Vector4>()), Times.Exactly(3));
    }
}
