// <copyright file="ProjectorComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests;

using FluentAssertions;
using Moq;
using Scenery.Components.Implementations;
using Scenery.Components.Interfaces;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;
using Scenery.Models.Scenes;
using Xunit;

/// <summary>
/// Provides Tests for <see cref="ProjectorComponent"/>.
/// </summary>
public class ProjectorComponentTests
{
    private readonly ProjectorComponent systemUnderTest;
    private readonly Mock<IColorComponent> colorComponentTestDouble;
    private readonly Mock<IFuncVector2Vector3Component> funcVector2Vector3ComponentTestDouble;
    private readonly Mock<ISceneComponentFactory> sceneComponentFactoryTestDouble;
    private readonly Mock<IVector3Component> vector3ComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectorComponentTests"/> class.
    /// </summary>
    public ProjectorComponentTests()
    {
        this.colorComponentTestDouble = new Mock<IColorComponent>();
        this.funcVector2Vector3ComponentTestDouble = new Mock<IFuncVector2Vector3Component>();
        this.sceneComponentFactoryTestDouble = new Mock<ISceneComponentFactory>();
        this.vector3ComponentTestDouble = new Mock<IVector3Component>();
        this.systemUnderTest = new ProjectorComponent(
            this.colorComponentTestDouble.Object,
            this.funcVector2Vector3ComponentTestDouble.Object,
            this.sceneComponentFactoryTestDouble.Object,
            this.vector3ComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="ProjectorComponent.ProjectSceneToImage(Scene, ProjectorSettings)"/>.
    /// </summary>
    [Fact]
    public void GivenTheProjectorSettingsIsNullWhenProjectSceneToImageIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var projectorSettings = default(ProjectorSettings);

        // Act.
        var action = () => this.systemUnderTest.ProjectSceneToImage(new Scene(), projectorSettings);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="ProjectorComponent.ProjectSceneToImage(Scene, ProjectorSettings)"/>.
    /// </summary>
    [Fact]
    public void GivenNoSurfaceIntersectionsWhenProjectSceneToImageIsCalledThenTheBackgroundColorIsReturned()
    {
        // Arrange.
        var scene = new Scene();
        var projectorSettings = new ProjectorSettings
        {
            Eye = new Vector3(),
            Focus = new Vector3(),
            FieldOfView = Math.PI / 4D,
            Background = new Color(),
        };
        this.funcVector2Vector3ComponentTestDouble
            .Setup(component => component.CreatePlane(It.IsAny<Vector3>(), It.IsAny<Vector3>(), It.IsAny<Vector3>()))
            .Returns(point => new Vector3());
        var sceneComponentTestDouble = new Mock<ISceneComponent>();
        sceneComponentTestDouble
            .Setup(component => component.GetAllSurfaceIntersections(It.IsAny<Line3>()))
            .Returns(new List<SurfaceIntersection>());
        this.sceneComponentFactoryTestDouble
            .Setup(component => component.CreateSceneComponent(It.IsAny<Scene>()))
            .Returns(sceneComponentTestDouble.Object);

        // Act.
        var result = this.systemUnderTest.ProjectSceneToImage(scene, projectorSettings);

        // Assert.
        result(new Vector2()).Should().Be(projectorSettings.Background);
        this.funcVector2Vector3ComponentTestDouble
            .Verify(component => component.CreatePlane(It.IsAny<Vector3>(), It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        this.sceneComponentFactoryTestDouble
            .Verify(component => component.CreateSceneComponent(It.IsAny<Scene>()), Times.Once);
        this.vector3ComponentTestDouble
            .Verify(component => component.Add(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        this.vector3ComponentTestDouble
            .Verify(component => component.CrossProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(2));
        this.vector3ComponentTestDouble
            .Verify(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()), Times.Exactly(2));
        this.vector3ComponentTestDouble
            .Verify(component => component.Normalize(It.IsAny<Vector3>()), Times.Exactly(4));
        this.vector3ComponentTestDouble
            .Verify(component => component.Subtract(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(2));
    }

    /// <summary>
    /// Tests <see cref="ProjectorComponent.ProjectSceneToImage(Scene, ProjectorSettings)"/>.
    /// </summary>
    [Fact]
    public void GivenASurfaceIntersectionBehindTheEyeWhenProjectSceneToImageIsCalledThenTheBackgroundColorIsReturned()
    {
        // Arrange.
        var scene = new Scene();
        var projectorSettings = new ProjectorSettings();
        this.funcVector2Vector3ComponentTestDouble
            .Setup(component => component.CreatePlane(It.IsAny<Vector3>(), It.IsAny<Vector3>(), It.IsAny<Vector3>()))
            .Returns(point => new Vector3());
        var sceneComponentTestDouble = new Mock<ISceneComponent>();
        sceneComponentTestDouble
            .Setup(component => component.GetAllSurfaceIntersections(It.IsAny<Line3>()))
            .Returns(new List<SurfaceIntersection> { new SurfaceIntersection { Distance = -1D } });
        this.sceneComponentFactoryTestDouble
            .Setup(component => component.CreateSceneComponent(It.IsAny<Scene>()))
            .Returns(sceneComponentTestDouble.Object);

        // Act.
        var result = this.systemUnderTest.ProjectSceneToImage(scene, projectorSettings);

        // Assert.
        result(new Vector2()).Should().Be(projectorSettings.Background);
        this.funcVector2Vector3ComponentTestDouble
            .Verify(component => component.CreatePlane(It.IsAny<Vector3>(), It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        this.sceneComponentFactoryTestDouble
            .Verify(component => component.CreateSceneComponent(It.IsAny<Scene>()), Times.Once);
        this.vector3ComponentTestDouble
            .Verify(component => component.Add(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        this.vector3ComponentTestDouble
            .Verify(component => component.CrossProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(2));
        this.vector3ComponentTestDouble
            .Verify(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()), Times.Exactly(2));
        this.vector3ComponentTestDouble
            .Verify(component => component.Normalize(It.IsAny<Vector3>()), Times.Exactly(4));
        this.vector3ComponentTestDouble
            .Verify(component => component.Subtract(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(2));
    }

    /// <summary>
    /// Tests <see cref="ProjectorComponent.ProjectSceneToImage(Scene, ProjectorSettings)"/>.
    /// </summary>
    [Fact]
    public void GivenTwoSurfaceIntersectionsWhenProjectSceneToImageIsCalledThenTheCorrectColorIsReturned()
    {
        // Arrange.
        var scene = new Scene();
        var projectorSettings = new ProjectorSettings();
        this.funcVector2Vector3ComponentTestDouble
            .Setup(component => component.CreatePlane(It.IsAny<Vector3>(), It.IsAny<Vector3>(), It.IsAny<Vector3>()))
            .Returns(point => new Vector3());
        var sceneComponentTestDouble = new Mock<ISceneComponent>();
        sceneComponentTestDouble
            .Setup(component => component.GetAllSurfaceIntersections(It.IsAny<Line3>()))
            .Returns(new List<SurfaceIntersection>
            {
                    new SurfaceIntersection { Distance = 1D },
                    new SurfaceIntersection { Distance = 2D },
            });
        this.sceneComponentFactoryTestDouble
            .Setup(component => component.CreateSceneComponent(It.IsAny<Scene>()))
            .Returns(sceneComponentTestDouble.Object);
        var expectedColor = new Color();
        this.colorComponentTestDouble
            .Setup(component => component.Multiply(It.IsAny<Color>(), It.IsAny<double>()))
            .Returns(expectedColor);

        // Act.
        var result = this.systemUnderTest.ProjectSceneToImage(scene, projectorSettings);

        // Assert.
        result(new Vector2()).Should().Be(expectedColor);
        this.colorComponentTestDouble
            .Verify(component => component.Multiply(It.IsAny<Color>(), It.IsAny<double>()), Times.Once);
        this.funcVector2Vector3ComponentTestDouble
            .Verify(component => component.CreatePlane(It.IsAny<Vector3>(), It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        this.sceneComponentFactoryTestDouble
            .Verify(component => component.CreateSceneComponent(It.IsAny<Scene>()), Times.Once);
        this.vector3ComponentTestDouble
            .Verify(component => component.Add(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        this.vector3ComponentTestDouble
            .Verify(component => component.CrossProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(2));
        this.vector3ComponentTestDouble
            .Verify(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        this.vector3ComponentTestDouble
            .Verify(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()), Times.Exactly(2));
        this.vector3ComponentTestDouble
            .Verify(component => component.Normalize(It.IsAny<Vector3>()), Times.Exactly(4));
        this.vector3ComponentTestDouble
            .Verify(component => component.Subtract(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(2));
    }
}
