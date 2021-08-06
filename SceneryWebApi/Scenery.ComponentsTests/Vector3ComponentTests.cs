// <copyright file="Vector3ComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests
{
    using System;
    using FluentAssertions;
    using Scenery.Components.Implementations;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="Vector3Component"/>.
    /// </summary>
    public class Vector3ComponentTests
    {
        private readonly Vector3Component systemUnderTest;
        private readonly DoubleEquivalencyTestComponent doubleEquivalencyTestComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3ComponentTests"/> class.
        /// </summary>
        public Vector3ComponentTests()
        {
            this.systemUnderTest = new Vector3Component();
            this.doubleEquivalencyTestComponent = new DoubleEquivalencyTestComponent();
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.Add(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheVectorIsNullWhenAddIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action Add(Vector3 vector)
            {
                return () => this.systemUnderTest.Add(vector, new Vector3());
            }

            // Act.
            var action = Add(new Vector3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = Add(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.Add(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheOtherVectorIsNullWhenAddIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action Add(Vector3 otherVector)
            {
                return () => this.systemUnderTest.Add(new Vector3(), otherVector);
            }

            // Act.
            var action = Add(new Vector3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = Add(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.Add(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTwoVectorsWhenAddIsCalledThenTheCorrectSumIsReturned()
        {
            // Arrange.
            var vector = new Vector3
            {
                XCoordinate = 1D,
                YCoordinate = 2D,
                ZCoordinate = 3D,
            };

            var otherVector = new Vector3
            {
                XCoordinate = 4D,
                YCoordinate = 5D,
                ZCoordinate = 6D,
            };

            // Act.
            var result = this.systemUnderTest.Add(vector, otherVector);

            // Assert.
            result.Should().BeEquivalentTo(
                new Vector3
                {
                    XCoordinate = 5D,
                    YCoordinate = 7D,
                    ZCoordinate = 9D,
                },
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.CreateVector3FromSphericalCoordinates(double, double, double)"/>.
        /// </summary>
        [Fact]
        public void GivenSphericalComponentsWhenCreateVector3FromSphericalComponentsIsCalledThenTheCorrectVectorIsCreated()
        {
            // Arrange.
            var radius = 1D;
            var inclination = 0D;
            var azimuth = 0D;

            // Act.
            var result = this.systemUnderTest.CreateVector3FromSphericalCoordinates(radius, inclination, azimuth);

            // Assert.
            result.Should().BeEquivalentTo(
                new Vector3
                {
                    XCoordinate = 0D,
                    YCoordinate = 0D,
                    ZCoordinate = 1D,
                },
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.CrossProduct(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheVectorIsNullWhenCrossProductIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action CrossProduct(Vector3 vector)
            {
                return () => this.systemUnderTest.CrossProduct(vector, new Vector3());
            }

            // Act.
            var action = CrossProduct(new Vector3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = CrossProduct(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.CrossProduct(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheOtherVectorIsNullWhenCrossProductIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action Crossproduct(Vector3 otherVector)
            {
                return () => this.systemUnderTest.CrossProduct(new Vector3(), otherVector);
            }

            // Act.
            var action = Crossproduct(new Vector3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = Crossproduct(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.CrossProduct(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTwoVectorsWhenCrossProductIsCalledThenTheCorrectCrossProductIsReturned()
        {
            // Arrange.
            var vector = new Vector3
            {
                XCoordinate = 1D,
                YCoordinate = 2D,
                ZCoordinate = 3D,
            };

            var otherVector = new Vector3
            {
                XCoordinate = 4D,
                YCoordinate = 5D,
                ZCoordinate = 6D,
            };

            // Act.
            var result = this.systemUnderTest.CrossProduct(vector, otherVector);

            // Assert.
            result.Should().BeEquivalentTo(
                new Vector3
                {
                    XCoordinate = -3D,
                    YCoordinate = 6D,
                    ZCoordinate = -3D,
                },
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.Divide(Vector3, double)"/>.
        /// </summary>
        [Fact]
        public void GivenAVectorAndADivisorWhenDivideIsCalledThenTheCorrectQuotientIsReturned()
        {
            // Arrange.
            var vector = new Vector3
            {
                XCoordinate = 1D,
                YCoordinate = 2D,
                ZCoordinate = 3D,
            };

            var divisor = 2D;

            // Act.
            var result = this.systemUnderTest.Divide(vector, divisor);

            // Assert.
            result.Should().BeEquivalentTo(
                new Vector3
                {
                    XCoordinate = 0.5D,
                    YCoordinate = 1D,
                    ZCoordinate = 1.5D,
                },
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.DotProduct(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheVectorIsNullWhenDotProductIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action DotProduct(Vector3 vector)
            {
                return () => this.systemUnderTest.DotProduct(vector, new Vector3());
            }

            // Act.
            var action = DotProduct(new Vector3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = DotProduct(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.DotProduct(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheOtherVectorIsNullWhenDotProductIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action DotProduct(Vector3 otherVector)
            {
                return () => this.systemUnderTest.DotProduct(new Vector3(), otherVector);
            }

            // Act.
            var action = DotProduct(new Vector3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = DotProduct(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.DotProduct(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTwoVectorsWhenDotProductIsCalledThenTheCorrectDotProductIsReturned()
        {
            // Arrange.
            var vector = new Vector3
            {
                XCoordinate = 1D,
                YCoordinate = 2D,
                ZCoordinate = 3D,
            };

            var otherVector = new Vector3
            {
                XCoordinate = 4D,
                YCoordinate = 5D,
                ZCoordinate = 6D,
            };

            // Act.
            var result = this.systemUnderTest.DotProduct(vector, otherVector);

            // Assert.
            result.Should().BeApproximately(32D, this.doubleEquivalencyTestComponent.Precision);
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.GetLength(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAVectorWhenGetLengthIsCalledThenTheCorrectLengthIsReturned()
        {
            // Arrange.
            var vector = new Vector3
            {
                XCoordinate = 1D,
                YCoordinate = 2D,
                ZCoordinate = 3D,
            };

            // Act.
            var result = this.systemUnderTest.GetLength(vector);

            // Assert.
            result.Should().BeApproximately(3.742D, this.doubleEquivalencyTestComponent.Precision);
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.Multiply(Vector3, double)"/>.
        /// </summary>
        [Fact]
        public void GivenTheVectorIsNullWhenMultiplyIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action Multiply(Vector3 vector)
            {
                return () => this.systemUnderTest.Multiply(vector, 0D);
            }

            // Act.
            var action = Multiply(new Vector3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = Multiply(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.Multiply(Vector3, double)"/>.
        /// </summary>
        [Fact]
        public void GivenAVectorAndAFactorWhenMultiplyIsCalledThenTheCorrectProductIsReturned()
        {
            // Arrange.
            var vector = new Vector3
            {
                XCoordinate = 1D,
                YCoordinate = 2D,
                ZCoordinate = 3D,
            };

            var factor = 2D;

            // Act.
            var result = this.systemUnderTest.Multiply(vector, factor);

            // Assert.
            result.Should().BeEquivalentTo(
                new Vector3
                {
                    XCoordinate = 2D,
                    YCoordinate = 4D,
                    ZCoordinate = 6D,
                },
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.Normalize(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAVectorWhenNormalizeIsCalledThenTheCorrectlyNormalizedVectorIsReturned()
        {
            // Arrange.
            var vector = new Vector3
            {
                XCoordinate = 1D,
                YCoordinate = 2D,
                ZCoordinate = 3D,
            };

            // Act.
            var result = this.systemUnderTest.Normalize(vector);

            // Assert.
            result.Should().BeEquivalentTo(
                new Vector3
                {
                    XCoordinate = 0.267D,
                    YCoordinate = 0.535D,
                    ZCoordinate = 0.802D,
                },
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.Subtract(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheVectorIsNullWhenSubtractIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action Subtract(Vector3 vector)
            {
                return () => this.systemUnderTest.Subtract(vector, new Vector3());
            }

            // Act.
            var action = Subtract(new Vector3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = Subtract(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.Subtract(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTheOtherVectorIsNullWhenSubtractIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action Subtract(Vector3 otherVector)
            {
                return () => this.systemUnderTest.Subtract(new Vector3(), otherVector);
            }

            // Act.
            var action = Subtract(new Vector3());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = Subtract(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="Vector3Component.Subtract(Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenTwoVectorsWhenSubtractIsCalledThenTheCorrectDifferenceIsReturned()
        {
            // Arrange.
            var vector = new Vector3
            {
                XCoordinate = 1D,
                YCoordinate = 2D,
                ZCoordinate = 3D,
            };

            var otherVector = new Vector3
            {
                XCoordinate = 4D,
                YCoordinate = 5D,
                ZCoordinate = 6D,
            };

            // Act.
            var result = this.systemUnderTest.Subtract(vector, otherVector);

            // Assert.
            result.Should().BeEquivalentTo(
                new Vector3
                {
                    XCoordinate = -3D,
                    YCoordinate = -3D,
                    ZCoordinate = -3D,
                },
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
        }
    }
}
