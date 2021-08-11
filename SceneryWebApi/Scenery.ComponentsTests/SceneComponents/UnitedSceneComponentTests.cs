// <copyright file="UnitedSceneComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponents
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Moq;
    using Scenery.Components.Implementations.SceneComponents;
    using Scenery.Components.Interfaces;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="UnitedSceneComponent"/>.
    /// </summary>
    public class UnitedSceneComponentTests
    {
        private readonly UnitedSceneComponent systemUnderTest;
        private readonly Mock<ILine3Component> line3ComponentTestDouble;
        private readonly Mock<ISceneComponent> originalSceneComponentTestDouble;
        private readonly Mock<ISceneComponent> otherOriginalSceneComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitedSceneComponentTests"/> class.
        /// </summary>
        public UnitedSceneComponentTests()
        {
            this.line3ComponentTestDouble = new Mock<ILine3Component>();
            this.originalSceneComponentTestDouble = new Mock<ISceneComponent>();
            this.otherOriginalSceneComponentTestDouble = new Mock<ISceneComponent>();
            this.systemUnderTest = new UnitedSceneComponent(
                this.line3ComponentTestDouble.Object,
                this.originalSceneComponentTestDouble.Object,
                this.otherOriginalSceneComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="UnitedSceneComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheFirstOriginalSceneContainsThePointWhenContainsIsCalledThenTrueIsReturned()
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
        /// Tests <see cref="UnitedSceneComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenBothOriginalScenesDoNotContainThePointWhenContainsIsCalledThenFalseIsReturned()
        {
            // Arrange.
            var point = new Vector3();
            this.originalSceneComponentTestDouble
                .Setup(component => component.Contains(It.IsAny<Vector3>()))
                .Returns(false);
            this.otherOriginalSceneComponentTestDouble
                .Setup(component => component.Contains(It.IsAny<Vector3>()))
                .Returns(false);

            // Act.
            var result = this.systemUnderTest.Contains(point);

            // Assert.
            result.Should().BeFalse();
            this.originalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
            this.otherOriginalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="UnitedSceneComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenOnlyTheSecondOriginalSceneContainsThePointWhenContainsIsCalledThenTrueIsReturned()
        {
            // Arrange.
            var point = new Vector3();
            this.originalSceneComponentTestDouble
                .Setup(component => component.Contains(It.IsAny<Vector3>()))
                .Returns(false);
            this.otherOriginalSceneComponentTestDouble
                .Setup(component => component.Contains(It.IsAny<Vector3>()))
                .Returns(true);

            // Act.
            var result = this.systemUnderTest.Contains(point);

            // Assert.
            result.Should().BeTrue();
            this.originalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
            this.otherOriginalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="UnitedSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheLineOfSightIsNullWhenGetAllInterceptsIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action GetAllIntercepts(Line3 lineOfSight)
            {
                return () => this.systemUnderTest.GetAllIntercepts(lineOfSight);
            }

            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>());
            this.otherOriginalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>());

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
        /// Tests <see cref="UnitedSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenBothOriginalScenesGiveNoInterceptsWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>());
            this.otherOriginalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>());

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().BeEmpty();
            this.originalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
            this.otherOriginalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="UnitedSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheFirstOriginalSceneGivesAnInterceptWhichIsNotContainedByTheSecondOriginalSceneWhenGetAllInterceptsIsCalledThenOneInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>
                {
                    new Intercept(),
                });
            this.otherOriginalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>());
            this.otherOriginalSceneComponentTestDouble
                .Setup(component => component.Contains(It.IsAny<Vector3>()))
                .Returns(false);

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().HaveCount(1);
            this.line3ComponentTestDouble
                .Verify(component => component.GetPointAtDistance(It.IsAny<Line3>(), It.IsAny<double>()), Times.Once);
            this.originalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
            this.otherOriginalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
            this.otherOriginalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="UnitedSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheFirstOriginalSceneGivesAnInterceptWhichIsContainedByTheSecondOriginalSceneWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>
                {
                    new Intercept(),
                });
            this.otherOriginalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>());
            this.otherOriginalSceneComponentTestDouble
                .Setup(component => component.Contains(It.IsAny<Vector3>()))
                .Returns(true);

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().BeEmpty();
            this.line3ComponentTestDouble
                .Verify(component => component.GetPointAtDistance(It.IsAny<Line3>(), It.IsAny<double>()), Times.Once);
            this.originalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
            this.otherOriginalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
            this.otherOriginalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="UnitedSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheSecondOriginalSceneGivesAnInterceptWhichIsNotContainedByTheFirstOriginalSceneWhenGetAllInterceptsIsCalledThenOneInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>());
            this.originalSceneComponentTestDouble
                .Setup(component => component.Contains(It.IsAny<Vector3>()))
                .Returns(false);
            this.otherOriginalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>
                {
                    new Intercept(),
                });

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().HaveCount(1);
            this.line3ComponentTestDouble
                .Verify(component => component.GetPointAtDistance(It.IsAny<Line3>(), It.IsAny<double>()), Times.Once);
            this.originalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
            this.originalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
            this.otherOriginalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="UnitedSceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheSecondOriginalSceneGivesAnInterceptWhichIsContainedByTheFirstOriginalSceneWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();
            this.originalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>());
            this.originalSceneComponentTestDouble
                .Setup(component => component.Contains(It.IsAny<Vector3>()))
                .Returns(true);
            this.otherOriginalSceneComponentTestDouble
                .Setup(component => component.GetAllIntercepts(It.IsAny<Line3>()))
                .Returns(new List<Intercept>
                {
                    new Intercept(),
                });

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().BeEmpty();
            this.line3ComponentTestDouble
                .Verify(component => component.GetPointAtDistance(It.IsAny<Line3>(), It.IsAny<double>()), Times.Once);
            this.originalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
            this.originalSceneComponentTestDouble
                .Verify(component => component.Contains(It.IsAny<Vector3>()), Times.Once);
            this.otherOriginalSceneComponentTestDouble
                .Verify(component => component.GetAllIntercepts(It.IsAny<Line3>()), Times.Once);
        }
    }
}
