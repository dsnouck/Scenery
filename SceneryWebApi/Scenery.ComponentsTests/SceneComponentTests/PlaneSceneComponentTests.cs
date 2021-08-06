// <copyright file="PlaneSceneComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponentTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Moq;
    using Scenery.Components.Implementations.SceneComponents;
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="PlaneSceneComponent"/>.
    /// </summary>
    public class PlaneSceneComponentTests
    {
        private readonly PlaneSceneComponent systemUnderTest;
        private readonly Mock<IVector3Component> vector3ComponentTestDouble;
        private readonly Vector3 normal;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaneSceneComponentTests"/> class.
        /// </summary>
        public PlaneSceneComponentTests()
        {
            this.vector3ComponentTestDouble = new Mock<IVector3Component>();
            this.normal = new Vector3();
            this.systemUnderTest = new PlaneSceneComponent(
                this.vector3ComponentTestDouble.Object,
                this.normal);
        }

        /// <summary>
        /// Tests <see cref="PlaneSceneComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAPointBelowThePlaneWhenContainsIsCalledThenTrueIsReturned()
        {
            // Arrange.
            var point = new Vector3();
            this.vector3ComponentTestDouble
                .Setup(component => component.DotProduct(point, this.normal))
                .Returns(0D);
            this.vector3ComponentTestDouble
                .Setup(component => component.DotProduct(this.normal, point))
                .Returns(1D);

            // Act.
            var result = this.systemUnderTest.Contains(point);

            // Assert.
            result.Should().BeTrue();
            this.vector3ComponentTestDouble
                .Verify(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(2));
        }

        /// <summary>
        /// Tests <see cref="PlaneSceneComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAPointAboveThePlaneWhenContainsIsCalledThenFalseIsReturned()
        {
            // Arrange.
            var point = new Vector3();
            this.vector3ComponentTestDouble
                .Setup(component => component.DotProduct(point, this.normal))
                .Returns(1D);
            this.vector3ComponentTestDouble
                .Setup(component => component.DotProduct(this.normal, point))
                .Returns(0D);

            // Act.
            var result = this.systemUnderTest.Contains(point);

            // Assert.
            result.Should().BeFalse();
            this.vector3ComponentTestDouble
                .Verify(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(2));
        }

        /// <summary>
        /// Tests <see cref="PlaneSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheLineOfSightIsNullWhenGetAllInterceptsIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action GetAllIntercepts(Line3 lineOfSight)
            {
                return () => this.systemUnderTest.GetAllIntercepts(lineOfSight);
            }

            // Act.
            var action = GetAllIntercepts(new Line3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = GetAllIntercepts(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="PlaneSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheLineOfSightIsApproximatelyParallelToThePlaneWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            this.vector3ComponentTestDouble
                .Setup(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()))
                .Returns(0D);

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().BeEmpty();
            this.vector3ComponentTestDouble
                .Verify(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="PlaneSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenALineOfSightWhenGetAllInterceptsIsCalledThenASingleInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            this.vector3ComponentTestDouble
                .Setup(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()))
                .Returns(1D);
            this.vector3ComponentTestDouble
                .Setup(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()))
                .Returns(new Vector3());

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().HaveCount(1);
            result.Single().Normal().Should().NotBeNull();
            this.vector3ComponentTestDouble
                .Verify(component => component.DotProduct(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(2));
            this.vector3ComponentTestDouble
                .Verify(component => component.GetLength(It.IsAny<Vector3>()), Times.Once);
            this.vector3ComponentTestDouble
                .Verify(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()), Times.Once);
            this.vector3ComponentTestDouble
                .Verify(component => component.Normalize(It.IsAny<Vector3>()), Times.Once);
            this.vector3ComponentTestDouble
                .Verify(component => component.Subtract(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        }
    }
}
