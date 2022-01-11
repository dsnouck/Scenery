// <copyright file="ProjectorSettingsValidatorTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.ControllersTests.Validators;

using FluentValidation.TestHelper;
using Moq;
using Scenery.Components.Interfaces;
using Scenery.Controllers.Validators;
using Scenery.Models;
using Xunit;

/// <summary>
/// Provides tests for <see cref="ProjectorSettingsValidator"/>.
/// </summary>
public class ProjectorSettingsValidatorTests
{
    private readonly SceneContainerValidator systemUnderTest;
    private readonly Mock<IVector3Component> vector3ComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectorSettingsValidatorTests"/> class.
    /// </summary>
    public ProjectorSettingsValidatorTests()
    {
        this.vector3ComponentTestDouble = new Mock<IVector3Component>();
        this.vector3ComponentTestDouble
            .Setup(vector3Component => vector3Component.GetLength(It.IsAny<Vector3>()))
            .Returns(1D);

        this.systemUnderTest = new SceneContainerValidator(this.vector3ComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="ProjectorSettingsValidator"/>.
    /// </summary>
    [Fact]
    public void GivenEyeIsNullWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Projector = new ProjectorSettings
            {
                Eye = null,
            },
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => sceneContainer.Projector.Eye);
    }

    /// <summary>
    /// Tests <see cref="ProjectorSettingsValidator"/>.
    /// </summary>
    [Fact]
    public void GivenFocusIsNullWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Projector = new ProjectorSettings
            {
                Focus = null,
            },
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => sceneContainer.Projector.Focus);
    }

    /// <summary>
    /// Tests <see cref="ProjectorSettingsValidator"/>.
    /// </summary>
    [Fact]
    public void GivenEyeAndFocusCoincideWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer();
        this.vector3ComponentTestDouble
            .Setup(vector3Component => vector3Component.GetLength(It.IsAny<Vector3>()))
            .Returns(0D);

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => sceneContainer.Projector);
    }

    /// <summary>
    /// Tests <see cref="ProjectorSettingsValidator"/>.
    /// </summary>
    [Fact]
    public void GivenBackgroundIsNullWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Projector = new ProjectorSettings
            {
                Background = null,
            },
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => sceneContainer.Projector.Background);
    }
}
