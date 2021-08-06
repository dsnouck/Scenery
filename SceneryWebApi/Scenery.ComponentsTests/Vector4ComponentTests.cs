﻿// <copyright file="Vector4ComponentTests.cs" company="dsnouck">
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
    /// Provides tests for <see cref="Vector4Component"/>.
    /// </summary>
    public class Vector4ComponentTests
    {
        private readonly Vector4Component systemUnderTest;
        private readonly DoubleEquivalencyTestComponent doubleEquivalencyTestComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4ComponentTests"/> class.
        /// </summary>
        public Vector4ComponentTests()
        {
            this.systemUnderTest = new Vector4Component();
            this.doubleEquivalencyTestComponent = new DoubleEquivalencyTestComponent();
        }

        /// <summary>
        /// Tests <see cref="Vector4Component.DotProduct(Vector4, Vector4)"/>.
        /// </summary>
        [Fact]
        public void GivenTheVectorIsNullWhenDotProductIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action DotProduct(Vector4 vector)
            {
                return () => this.systemUnderTest.DotProduct(vector, new Vector4());
            }

            // Act.
            var action = DotProduct(new Vector4());

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
        /// Tests <see cref="Vector4Component.DotProduct(Vector4, Vector4)"/>.
        /// </summary>
        [Fact]
        public void GivenTheOtherVectorIsNullWhenDotProductIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action DotProduct(Vector4 otherVector)
            {
                return () => this.systemUnderTest.DotProduct(new Vector4(), otherVector);
            }

            // Act.
            var action = DotProduct(new Vector4());

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
        /// Tests <see cref="Vector4Component.DotProduct(Vector4, Vector4)"/>.
        /// </summary>
        [Fact]
        public void GivenTwoVectorsWhenDotProductIsCalledThenTheCorrectDotProductIsReturned()
        {
            // Arrange.
            var vector = new Vector4
            {
                XCoordinate = 1D,
                YCoordinate = 2D,
                ZCoordinate = 3D,
                WCoordinate = 4D,
            };
            var otherVector = new Vector4
            {
                XCoordinate = 5D,
                YCoordinate = 6D,
                ZCoordinate = 7D,
                WCoordinate = 8D,
            };

            // Act.
            var result = this.systemUnderTest.DotProduct(vector, otherVector);

            // Assert.
            result.Should().BeApproximately(70D, this.doubleEquivalencyTestComponent.Precision);
        }
    }
}
