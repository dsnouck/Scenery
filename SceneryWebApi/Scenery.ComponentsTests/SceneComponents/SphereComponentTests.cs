// <copyright file="SphereComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Moq;
    using Scenery.Components.Implementations.SceneComponents;
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="SphereComponent"/>.
    /// </summary>
    public class SphereComponentTests
    {
        private readonly SphereComponent systemUnderTest;
        private readonly Mock<IFuncDoubleDoubleComponent> funcDoubleDoubleComponentTestDouble;
        private readonly Mock<ILine3Component> line3ComponentTestDouble;
        private readonly Mock<IVector3Component> vector3ComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="SphereComponentTests"/> class.
        /// </summary>
        public SphereComponentTests()
        {
            this.funcDoubleDoubleComponentTestDouble = new Mock<IFuncDoubleDoubleComponent>();
            this.line3ComponentTestDouble = new Mock<ILine3Component>();
            this.vector3ComponentTestDouble = new Mock<IVector3Component>();
            this.systemUnderTest = new SphereComponent(
                this.funcDoubleDoubleComponentTestDouble.Object,
                this.line3ComponentTestDouble.Object,
                this.vector3ComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="SphereComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAPointInsideTheSphereWhenContainsIsCalledThenTrueIsReturned()
        {
            // Arrange.
            var point = new Vector3();
            this.vector3ComponentTestDouble
                .Setup(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()))
                .Returns(0D);

            // Act.
            var result = this.systemUnderTest.Contains(point);

            // Assert.
            result.Should().BeTrue();
            this.vector3ComponentTestDouble
                .Verify(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="SphereComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAPointOutsideTheSphereWhenContainsIsCalledThenFalseIsReturned()
        {
            // Arrange.
            var point = new Vector3();
            this.vector3ComponentTestDouble
                .Setup(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()))
                .Returns(2D);

            // Act.
            var result = this.systemUnderTest.Contains(point);

            // Assert.
            result.Should().BeFalse();
            this.vector3ComponentTestDouble
                .Verify(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="SphereComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheLineOfSightIsNullWhenGetAllInterceptsIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Line3 lineOfSight = null;

            // Act.
            Action action = () => this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="SphereComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenNoInterceptsWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            var zeros = new List<double>();
            this.funcDoubleDoubleComponentTestDouble
                .Setup(component => component.GetRealZerosOfQuadraticFunction(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                .Returns(zeros);

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().BeEmpty();
            this.funcDoubleDoubleComponentTestDouble
                .Verify(component => component.GetRealZerosOfQuadraticFunction(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()), Times.Once);
            this.vector3ComponentTestDouble
                .Verify(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(3));
        }

        /// <summary>
        /// Tests <see cref="SphereComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTwoInterceptsWhenGetAllInterceptsIsCalledThenTwoInterceptsAreReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            var zeros = new List<double>
            {
                1D,
                2D,
            };
            this.funcDoubleDoubleComponentTestDouble
                .Setup(component => component.GetRealZerosOfQuadraticFunction(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
                .Returns(zeros);
            this.vector3ComponentTestDouble
                .Setup(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()))
                .Returns(new Vector3());

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().HaveCount(2);
            result.First().Normal().Should().NotBeNull();
            this.funcDoubleDoubleComponentTestDouble
                .Verify(component => component.GetRealZerosOfQuadraticFunction(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()), Times.Once);
            this.line3ComponentTestDouble
                .Verify(component => component.GetPointAtDistance(It.IsAny<Line3>(), It.IsAny<double>()), Times.Once);
            this.vector3ComponentTestDouble
                .Verify(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(3));
            this.vector3ComponentTestDouble
               .Verify(component => component.GetLength(It.IsAny<Vector3>()), Times.Once);
            this.vector3ComponentTestDouble
                .Verify(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()), Times.Once);
            this.vector3ComponentTestDouble
                .Verify(component => component.Normalize(It.IsAny<Vector3>()), Times.Once);
        }
    }
}
