// <copyright file="InvertedSceneComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponentTests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Moq;
    using Scenery.Components.Implementations.SceneComponents;
    using Scenery.Components.Interfaces;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="InvertedSceneComponent"/>.
    /// </summary>
    public class InvertedSceneComponentTests
    {
        private readonly InvertedSceneComponent systemUnderTest;
        private readonly Mock<IVector3Component> vector3ComponentTestDouble;
        private readonly Mock<ISceneComponent> originalSceneComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvertedSceneComponentTests"/> class.
        /// </summary>
        public InvertedSceneComponentTests()
        {
            this.vector3ComponentTestDouble = new Mock<IVector3Component>();
            this.originalSceneComponentTestDouble = new Mock<ISceneComponent>();
            this.systemUnderTest = new InvertedSceneComponent(
                this.vector3ComponentTestDouble.Object,
                this.originalSceneComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="InvertedSceneComponent.Contains(Vector3)"/>.
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
            result.Should().BeFalse();
            this.originalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="InvertedSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenALineOfSightWhenGetAllInterceptsIsCalledThenOriginalSceneComponentGetAllInterceptsIsCalled()
        {
            // Arrange.
            var lineOfSight = new Line3();
            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>
                {
                    new Intercept(),
                });
            this.vector3ComponentTestDouble
                .Setup(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()))
                .Returns(new Vector3());

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().HaveCount(1);
            result.Single().Normal().Should().NotBeNull();
            this.originalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
            this.vector3ComponentTestDouble
                .Verify(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()), Times.Once);
        }
    }
}
