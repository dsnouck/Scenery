// <copyright file="ColoredSceneValidatorTests.cs" company="dsnouck">
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
    /// Provides tests for <see cref="ColoredSceneValidator"/>.
    /// </summary>
    public class ColoredSceneValidatorTests
    {
        private readonly SceneContainerValidator systemUnderTest;
        private readonly Mock<IVector3Component> vector3ComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColoredSceneValidatorTests"/> class.
        /// </summary>
        public ColoredSceneValidatorTests()
        {
            this.vector3ComponentTestDouble = new Mock<IVector3Component>();
            this.vector3ComponentTestDouble
                .Setup(vector3Component => vector3Component.GetLength(It.IsAny<Vector3>()))
                .Returns(1D);

            this.systemUnderTest = new SceneContainerValidator(this.vector3ComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="ColoredSceneValidator"/>.
        /// </summary>
        [Fact]
        public void GivenColorIsNullWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new ColoredScene
                {
                    Color = null,
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as ColoredScene).Color);
        }

        /// <summary>
        /// Tests <see cref="ColoredSceneValidator"/>.
        /// </summary>
        [Fact]
        public void GivenOriginalSceneIsNullWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new ColoredScene
                {
                    OriginalScene = null,
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as ColoredScene).OriginalScene);
        }
    }
}
