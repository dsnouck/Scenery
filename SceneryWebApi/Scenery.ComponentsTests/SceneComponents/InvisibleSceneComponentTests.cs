// <copyright file="InvisibleSceneComponentTests.cs" company="dsnouck">
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
    /// Provides tests for <see cref="InvisibleSceneComponent"/>.
    /// </summary>
    public class InvisibleSceneComponentTests
    {
        private readonly InvisibleSceneComponent systemUnderTest;
        private readonly Mock<ISceneComponent> originalSceneComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvisibleSceneComponentTests"/> class.
        /// </summary>
        public InvisibleSceneComponentTests()
        {
            this.originalSceneComponentTestDouble = new Mock<ISceneComponent>();
            this.systemUnderTest = new InvisibleSceneComponent(
                this.originalSceneComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="InvisibleSceneComponent.Contains(Vector3)"/>.
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
        /// Tests <see cref="InvisibleSceneComponent.GetAllIntercepts(Line3)"/>.
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
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Never);
        }

        /// <summary>
        /// Tests <see cref="ColoredSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheOriginalSceneGivesOneInterceptWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
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
            result.Should().BeEmpty();
            this.originalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Never);
        }
    }
}
