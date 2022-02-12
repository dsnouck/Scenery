// <copyright file="ColorValidatorTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.ControllersTests.Validators;

using FluentValidation.TestHelper;
using Moq;
using Scenery.Components.Interfaces;
using Scenery.Controllers.Validators;
using Scenery.Models;
using Scenery.Models.Scenes;
using Xunit;

/// <summary>
/// Provides tests for <see cref="ColorValidator"/>.
/// </summary>
public class ColorValidatorTests
{
    private readonly SceneContainerValidator systemUnderTest;
    private readonly Mock<IVector3Component> vector3ComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorValidatorTests"/> class.
    /// </summary>
    public ColorValidatorTests()
    {
        this.vector3ComponentTestDouble = new Mock<IVector3Component>();
        this.vector3ComponentTestDouble
            .Setup(vector3Component => vector3Component.Length(It.IsAny<Vector3>()))
            .Returns(1D);

        this.systemUnderTest = new SceneContainerValidator(this.vector3ComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="ColorValidator"/>.
    /// </summary>
    [Fact]
    public void GivenRIsTooSmallWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Scene = new Colored
            {
                Color = new Color
                {
                    R = -1D,
                    G = 0D,
                    B = 0D,
                },
            },
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as Colored).Color.R);
    }

    /// <summary>
    /// Tests <see cref="ColorValidator"/>.
    /// </summary>
    [Fact]
    public void GivenRIsTooLargeWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Scene = new Colored
            {
                Color = new Color
                {
                    R = 2D,
                    G = 0D,
                    B = 0D,
                },
            },
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as Colored).Color.R);
    }

    /// <summary>
    /// Tests <see cref="ColorValidator"/>.
    /// </summary>
    [Fact]
    public void GivenGIsTooSmallWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Scene = new Colored
            {
                Color = new Color
                {
                    R = 0D,
                    G = -1D,
                    B = 0D,
                },
            },
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as Colored).Color.G);
    }

    /// <summary>
    /// Tests <see cref="ColorValidator"/>.
    /// </summary>
    [Fact]
    public void GivenGIsTooLargeWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Scene = new Colored
            {
                Color = new Color
                {
                    R = 0D,
                    G = 2D,
                    B = 0D,
                },
            },
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as Colored).Color.G);
    }

    /// <summary>
    /// Tests <see cref="ColorValidator"/>.
    /// </summary>
    [Fact]
    public void GivenBIsTooSmallWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Scene = new Colored
            {
                Color = new Color
                {
                    R = 0D,
                    G = 0D,
                    B = -1D,
                },
            },
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as Colored).Color.B);
    }

    /// <summary>
    /// Tests <see cref="ColorValidator"/>.
    /// </summary>
    [Fact]
    public void GivenBIsTooLargeWhenValidateIsCalledThenItFails()
    {
        // Arrange.
        var sceneContainer = new SceneContainer
        {
            Scene = new Colored
            {
                Color = new Color
                {
                    R = 0D,
                    G = 0D,
                    B = 2D,
                },
            },
        };

        // Act.
        var result = this.systemUnderTest.TestValidate(sceneContainer);

        // Assert.
        result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as Colored).Color.B);
    }
}
