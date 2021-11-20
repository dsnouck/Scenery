// <copyright file="InvertedComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponents
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
    /// Provides tests for <see cref="InvertedComponent"/>.
    /// </summary>
    public class InvertedComponentTests
    {
        private readonly InvertedComponent systemUnderTest;
        private readonly Mock<IVector3Component> vector3ComponentTestDouble;
        private readonly Mock<ISceneComponent> sceneComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvertedComponentTests"/> class.
        /// </summary>
        public InvertedComponentTests()
        {
            this.vector3ComponentTestDouble = new Mock<IVector3Component>();
            this.sceneComponentTestDouble = new Mock<ISceneComponent>();
            this.systemUnderTest = new InvertedComponent(
                this.vector3ComponentTestDouble.Object,
                this.sceneComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="InvertedComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAPointWhenContainsIsCalledThenSceneComponentContainsIsCalled()
        {
            // Arrange.
            var point = new Vector3();
            this.sceneComponentTestDouble
                .Setup(component => component.Contains(It.IsAny<Vector3>()))
                .Returns(true);

            // Act.
            var result = this.systemUnderTest.Contains(point);

            // Assert.
            result.Should().BeFalse();
            this.sceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="InvertedComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenALineOfSightWhenGetAllInterceptsIsCalledThenSceneComponentGetAllInterceptsIsCalled()
        {
            // Arrange.
            var lineOfSight = new Line3();
            this.sceneComponentTestDouble
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
            this.sceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
            this.vector3ComponentTestDouble
                .Verify(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()), Times.Once);
        }
    }
}
