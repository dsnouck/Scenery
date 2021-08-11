// <copyright file="AffinelyTransformedSceneComponentTests.cs" company="dsnouck">
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
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="AffinelyTransformedSceneComponent"/>.
    /// </summary>
    public class AffinelyTransformedSceneComponentTests
    {
        private readonly AffinelyTransformedSceneComponent systemUnderTest;
        private readonly Mock<IMatrix4Component> matrix4ComponentTestDouble;
        private readonly Mock<ISceneComponent> originalSceneComponentTestDouble;
        private readonly Matrix4 transformation;
        private readonly Matrix4 backwardTransformation;

        /// <summary>
        /// Initializes a new instance of the <see cref="AffinelyTransformedSceneComponentTests"/> class.
        /// </summary>
        public AffinelyTransformedSceneComponentTests()
        {
            this.matrix4ComponentTestDouble = new Mock<IMatrix4Component>();
            this.originalSceneComponentTestDouble = new Mock<ISceneComponent>();
            this.transformation = new Matrix4();
            this.backwardTransformation = new Matrix4();
            this.systemUnderTest = new AffinelyTransformedSceneComponent(
                this.matrix4ComponentTestDouble.Object,
                this.originalSceneComponentTestDouble.Object,
                this.transformation,
                this.backwardTransformation);
        }

        /// <summary>
        /// Tests <see cref="AffinelyTransformedSceneComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenThePointIsNullWhenContainsIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            this.matrix4ComponentTestDouble
                .Setup(component => component.Multiply(It.IsAny<Matrix4>(), It.IsAny<Vector4>()))
                .Returns(new Vector4());
            Action Contains(Vector3 point)
            {
                return () => this.systemUnderTest.Contains(point);
            }

            // Act.
            var action = Contains(new Vector3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = action = Contains(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="AffinelyTransformedSceneComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAPointWhenContainsIsCalledThenTheCorrectCalculationsArePerformed()
        {
            // Arrange.
            var point = new Vector3();
            this.matrix4ComponentTestDouble
                .Setup(component => component.Multiply(It.IsAny<Matrix4>(), It.IsAny<Vector4>()))
                .Returns(new Vector4());

            // Act.
            var result = this.systemUnderTest.Contains(point);

            // Assert.
            this.matrix4ComponentTestDouble
                .Verify(component => component.Multiply(It.IsAny<Matrix4>(), It.IsAny<Vector4>()), Times.Once);
            this.originalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="AffinelyTransformedSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheLineOfSightIsNullWhenGetAllInterceptsIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action GetAllIntercepts(Line3 lineOfSight)
            {
                return () => this.systemUnderTest.GetAllIntercepts(lineOfSight);
            }

            this.matrix4ComponentTestDouble
                .Setup(component => component.Multiply(It.IsAny<Matrix4>(), It.IsAny<Vector4>()))
                .Returns(new Vector4());
            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>
                {
                    new Intercept(),
                });

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
        /// Tests <see cref="AffinelyTransformedSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenALineOfSightWhenGetAllInterceptsIsCalledThenTheCorrectCalculationsArePerformed()
        {
            // Arrange.
            var lineOfSight = new Line3();
            this.matrix4ComponentTestDouble
                .Setup(component => component.Multiply(It.IsAny<Matrix4>(), It.IsAny<Vector4>()))
                .Returns(new Vector4());
            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>
                {
                    new Intercept(),
                });

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().HaveCount(1);
            result.Single().Normal().Should().NotBeNull();
            this.originalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
            this.matrix4ComponentTestDouble
                .Verify(component => component.Multiply(It.IsAny<Matrix4>(), It.IsAny<Vector4>()), Times.Exactly(3));
        }
    }
}
