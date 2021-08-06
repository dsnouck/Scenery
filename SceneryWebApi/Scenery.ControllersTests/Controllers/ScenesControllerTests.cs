// <copyright file="ScenesControllerTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.ControllersTests.Controllers
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Scenery.Components.Interfaces;
    using Scenery.Controllers.Controllers;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="ScenesController"/>.
    /// </summary>
    public class ScenesControllerTests
    {
        private readonly ScenesController systemUnderTest;
        private readonly Mock<ISceneContainerComponent> sceneContainerComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScenesControllerTests"/> class.
        /// </summary>
        public ScenesControllerTests()
        {
            this.sceneContainerComponentTestDouble = new Mock<ISceneContainerComponent>();
            this.systemUnderTest = new ScenesController(
                this.sceneContainerComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="ScenesController.Get"/>.
        /// </summary>
        [Fact]
        public void WhenGetIsCalledThenAnExampleSceneIsReturned()
        {
            // Act.
            var result = this.systemUnderTest.Get();

            // Assert.
            result.Should().BeOfType<OkObjectResult>();
            this.sceneContainerComponentTestDouble
                .Verify(component => component.GetExample(), Times.Once);
        }
    }
}
