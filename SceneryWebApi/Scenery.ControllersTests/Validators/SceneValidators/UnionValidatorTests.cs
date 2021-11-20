﻿// <copyright file="UnionValidatorTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.ControllersTests.Validators.SceneValidators
{
    using System.Collections.Generic;
    using FluentValidation.TestHelper;
    using Moq;
    using Scenery.Components.Interfaces;
    using Scenery.Controllers.Validators;
    using Scenery.Controllers.Validators.SceneValidators;
    using Scenery.Models;
    using Scenery.Models.Scenes;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="UnionValidator"/>.
    /// </summary>
    public class UnionValidatorTests
    {
        private readonly SceneContainerValidator systemUnderTest;
        private readonly Mock<IVector3Component> vector3ComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnionValidatorTests"/> class.
        /// </summary>
        public UnionValidatorTests()
        {
            this.vector3ComponentTestDouble = new Mock<IVector3Component>();
            this.vector3ComponentTestDouble
                .Setup(vector3Component => vector3Component.GetLength(It.IsAny<Vector3>()))
                .Returns(1D);

            this.systemUnderTest = new SceneContainerValidator(this.vector3ComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="UnionValidator"/>.
        /// </summary>
        [Fact]
        public void GivenScenesIsNullWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new Union
                {
                    Scenes = null,
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as Union).Scenes);
        }

        /// <summary>
        /// Tests <see cref="UnionValidator"/>.
        /// </summary>
        [Fact]
        public void GivenScenesContainsNullWhenValidateIsCalledThenItFails()
        {
            // Arrange.
            var sceneContainer = new SceneContainer
            {
                Scene = new Union
                {
                    Scenes = new List<Scene>
                    {
                        null,
                    },
                },
            };

            // Act.
            var result = this.systemUnderTest.TestValidate(sceneContainer);

            // Assert.
            result.ShouldHaveValidationErrorFor(sceneContainer => (sceneContainer.Scene as Union).Scenes);
        }
    }
}
