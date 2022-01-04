// <copyright file="SceneContainerValidatorTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.ControllersTests.Validators;

using System;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using Scenery.Components.Interfaces;
using Scenery.Controllers.Validators;
using Scenery.Models;
using Xunit;

/// <summary>
/// Provides tests for <see cref="SceneContainerValidator"/>.
/// </summary>
public class SceneContainerValidatorTests
{
    private readonly SceneContainerValidator systemUnderTest;
    private readonly Mock<IVector3Component> vector3ComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneContainerValidatorTests"/> class.
    /// </summary>
    public SceneContainerValidatorTests()
    {
        this.vector3ComponentTestDouble = new Mock<IVector3Component>();
        this.vector3ComponentTestDouble
            .Setup(vector3Component => vector3Component.GetLength(It.IsAny<Vector3>()))
            .Returns(1D);

        this.systemUnderTest = new SceneContainerValidator(this.vector3ComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="SceneContainerValidator"/>.
    /// </summary>
    [Fact]
    public void GivenSceneContainerIsNullWhenValidateIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        SceneContainer sceneContainer = null;

        // Act.
        Action action = () => this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="SceneContainerValidator"/>.
    /// </summary>
    [Fact]
    public void GivenSceneIsNullWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Scene = null,
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => sceneContainer.Scene);
    }

    /// <summary>
    /// Tests <see cref="SceneContainerValidator"/>.
    /// </summary>
    [Fact]
    public void GivenProjectorSettingsIsNullWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Projector = null,
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => sceneContainer.Projector);
    }

    /// <summary>
    /// Tests <see cref="SceneContainerValidator"/>.
    /// </summary>
    [Fact]
    public void GivenSamplerSettingsIsNullWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Sampler = null,
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => sceneContainer.Sampler);
    }
}
