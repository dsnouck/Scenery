// <copyright file="SceneContainerValidatorTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.ControllersTests.Validators
{
    using System;
    using FluentAssertions;
    using FluentValidation.TestHelper;
    using Scenery.Controllers.Validators;
    using Scenery.Models;
    using Scenery.Models.Scenes;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="SceneContainerValidator"/>.
    /// </summary>
    public class SceneContainerValidatorTests
    {
        private readonly SceneContainerValidator systemUnderTest;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneContainerValidatorTests"/> class.
        /// </summary>
        public SceneContainerValidatorTests()
        {
            this.systemUnderTest = new SceneContainerValidator();
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
        public void GivenSceneIsOfTypeSceneWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new Scene(),
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
        public void GivenOriginalSceneOfScaledSceneIsNullWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new ScaledScene
                {
                    OriginalScene = null,
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as ScaledScene).OriginalScene);
        }
    }
}
