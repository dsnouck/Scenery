// <copyright file="DoubleEquivalencyTestComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests
{
    using System;
    using FluentAssertions;
    using FluentAssertions.Equivalency;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="DoubleEquivalencyTestComponent"/>.
    /// </summary>
    public class DoubleEquivalencyTestComponentTests
    {
        private readonly DoubleEquivalencyTestComponent systemUnderTest;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleEquivalencyTestComponentTests"/> class.
        /// </summary>
        public DoubleEquivalencyTestComponentTests()
        {
            this.systemUnderTest = new DoubleEquivalencyTestComponent();
        }

        /// <summary>
        /// Tests <see cref="DoubleEquivalencyTestComponent.DoubleEquivalency{TEntity}(EquivalencyAssertionOptions{TEntity})"/>.
        /// </summary>
        [Fact]
        public void GivenTheOptionsAreNullWhenDoubleEquivalencyIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action DoubleEquivalency(EquivalencyAssertionOptions<Vector2> options)
            {
                return () => this.systemUnderTest.DoubleEquivalency(options);
            }

            // Act.
            var action = DoubleEquivalency(new EquivalencyAssertionOptions<Vector2>());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = DoubleEquivalency(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="DoubleEquivalencyTestComponent.DoubleEquivalency{TEntity}(EquivalencyAssertionOptions{TEntity})"/>.
        /// </summary>
        [Fact]
        public void GivenTwoNonequivalentVectorsWhenTheVectorsAreComparedThenTheVectorsAreFoundToBeNonequivalent()
        {
            // Arrange.
            var vector = new Vector2();
            var otherVector = new Vector2
            {
                XCoordinate = 0.01D,
            };

            // Assert.
            vector.Should().NotBeEquivalentTo(
                otherVector,
                this.systemUnderTest.DoubleEquivalency);
        }

        /// <summary>
        /// Tests <see cref="DoubleEquivalencyTestComponent.DoubleEquivalency{TEntity}(EquivalencyAssertionOptions{TEntity})"/>.
        /// </summary>
        [Fact]
        public void GivenTwoEquivalentVectorsWhenTheVectorsAreComparedThenTheVectorsAreFoundToBeEquivalent()
        {
            // Arrange.
            var vector = new Vector2();
            var otherVector = new Vector2
            {
                XCoordinate = 0.0001D,
            };

            // Assert.
            vector.Should().BeEquivalentTo(
                otherVector,
                this.systemUnderTest.DoubleEquivalency);
        }
    }
}
