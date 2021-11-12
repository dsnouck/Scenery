// <copyright file="ColorValidatorTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.ControllersTests.Validators
{
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
                .Setup(vector3Component => vector3Component.GetLength(It.IsAny<Vector3>()))
                .Returns(1D);

            this.systemUnderTest = new SceneContainerValidator(this.vector3ComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="ColorValidator"/>.
        /// </summary>
        [Fact]
        public void GivenRedComponentIsTooSmallWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new ColoredScene
                {
                    Color = new Color
                    {
                        RedComponent = -1D,
                        GreenComponent = 0D,
                        BlueComponent = 0D,
                    },
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as ColoredScene).Color.RedComponent);
        }

        /// <summary>
        /// Tests <see cref="ColorValidator"/>.
        /// </summary>
        [Fact]
        public void GivenRedComponentIsTooLargeWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new ColoredScene
                {
                    Color = new Color
                    {
                        RedComponent = 2D,
                        GreenComponent = 0D,
                        BlueComponent = 0D,
                    },
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as ColoredScene).Color.RedComponent);
        }

        /// <summary>
        /// Tests <see cref="ColorValidator"/>.
        /// </summary>
        [Fact]
        public void GivenGreenComponentIsTooSmallWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new ColoredScene
                {
                    Color = new Color
                    {
                        RedComponent = 0D,
                        GreenComponent = -1D,
                        BlueComponent = 0D,
                    },
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as ColoredScene).Color.GreenComponent);
        }

        /// <summary>
        /// Tests <see cref="ColorValidator"/>.
        /// </summary>
        [Fact]
        public void GivenGreenComponentIsTooLargeWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new ColoredScene
                {
                    Color = new Color
                    {
                        RedComponent = 0D,
                        GreenComponent = 2D,
                        BlueComponent = 0D,
                    },
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as ColoredScene).Color.GreenComponent);
        }

        /// <summary>
        /// Tests <see cref="ColorValidator"/>.
        /// </summary>
        [Fact]
        public void GivenBlueComponentIsTooSmallWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new ColoredScene
                {
                    Color = new Color
                    {
                        RedComponent = 0D,
                        GreenComponent = 0D,
                        BlueComponent = -1D,
                    },
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as ColoredScene).Color.BlueComponent);
        }

        /// <summary>
        /// Tests <see cref="ColorValidator"/>.
        /// </summary>
        [Fact]
        public void GivenBlueComponentIsTooLargeWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new ColoredScene
                {
                    Color = new Color
                    {
                        RedComponent = 0D,
                        GreenComponent = 0D,
                        BlueComponent = 2D,
                    },
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as ColoredScene).Color.BlueComponent);
        }
    }
}
