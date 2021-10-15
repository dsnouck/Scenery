// <copyright file="ColoredSceneComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponents
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Moq;
    using Scenery.Components.Implementations.SceneComponents;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="ColoredSceneComponent"/>.
    /// </summary>
    public class ColoredSceneComponentTests
    {
        private readonly ColoredSceneComponent systemUnderTest;
        private readonly Mock<ISceneComponent> originalSceneComponentTestDouble;
        private readonly Color color;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColoredSceneComponentTests"/> class.
        /// </summary>
        public ColoredSceneComponentTests()
        {
            this.originalSceneComponentTestDouble = new Mock<ISceneComponent>();
            this.color = new Color();
            this.systemUnderTest = new ColoredSceneComponent(
                this.originalSceneComponentTestDouble.Object,
                this.color);
        }

        /// <summary>
        /// Tests <see cref="ColoredSceneComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAPointWhenContainsIsCalledThenOriginalSceneComponentContainsIsCalled()
        {
            // Arrange.
            var point = new Vector3();
            this.originalSceneComponentTestDouble
                .Setup(component => component.Contains(It.IsAny<Vector3>()))
                .Returns(true);

            // Act.
            var result = this.systemUnderTest.Contains(point);

            // Assert.
            result.Should().BeTrue();
            this.originalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="ColoredSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheOriginalSceneGivesNoInterceptsWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            var originalIntercepts = new List<Intercept>();
            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(originalIntercepts);

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().BeEmpty();
            this.originalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="ColoredSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheOriginalSceneGivesOneInterceptWhenGetAllInterceptsIsCalledThenOneInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            var originalIntercepts = new List<Intercept>
            {
                new Intercept(),
            };
            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(originalIntercepts);

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().HaveCount(1);
            this.originalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        }
    }
}
