// <copyright file="ColorComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Scenery.Components.Implementations;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="ColorComponent"/>.
    /// </summary>
    public class ColorComponentTests
    {
        private readonly ColorComponent systemUnderTest;
        private readonly DoubleEquivalencyTestComponent doubleEquivalencyTestComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorComponentTests"/> class.
        /// </summary>
        public ColorComponentTests()
        {
            this.systemUnderTest = new ColorComponent();
            this.doubleEquivalencyTestComponent = new DoubleEquivalencyTestComponent();
        }

        /// <summary>
        /// Tests <see cref="ColorComponent.Average(List{Color})"/>.
        /// </summary>
        [Fact]
        public void GivenAListOfColorsWhenAverageIsCalledThenTheCorrectAverageIsReturned()
        {
            // Arrange.
            var colors = new List<Color>
            {
                new Color
                {
                    RedComponent = 0D,
                    GreenComponent = 0.5D,
                    BlueComponent = 1D,
                },
                new Color
                {
                    RedComponent = 1D,
                    GreenComponent = 0.5D,
                    BlueComponent = 0D,
                },
            };

            // Act.
            var result = this.systemUnderTest.Average(colors);

            // Assert.
            result.Should().BeEquivalentTo(
                new Color
                {
                    RedComponent = 0.5D,
                    GreenComponent = 0.5D,
                    BlueComponent = 0.5D,
                },
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
        }

        /// <summary>
        /// Tests <see cref="ColorComponent.GetSystemDrawingColorFromColor(Color)"/>.
        /// </summary>
        [Fact]
        public void GivenTheColorIsNullWhenGetSystemDrawingColorFromColorIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action GetSystemDrawingColorFromColor(Color color)
            {
                return () => this.systemUnderTest.GetSystemDrawingColorFromColor(color);
            }

            // Act.
            var action = GetSystemDrawingColorFromColor(new Color());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = GetSystemDrawingColorFromColor(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="ColorComponent.GetSystemDrawingColorFromColor(Color)"/>.
        /// </summary>
        [Fact]
        public void GivenAColorWhenGetSystemDrawingColorFromColorIsCalledThenTheCorrectSystemDrawingColorIsReturned()
        {
            // Arrange.
            var color = new Color
            {
                RedComponent = 0D,
                GreenComponent = 0.5D,
                BlueComponent = 1D,
            };

            // Act.
            var result = this.systemUnderTest.GetSystemDrawingColorFromColor(color);

            // Assert.
            result.Should().BeEquivalentTo(
                System.Drawing.Color.FromArgb(0, 127, 255));
        }

        /// <summary>
        /// Tests <see cref="ColorComponent.GetRedGreenBlueBytesFromColor(Color)"/>.
        /// </summary>
        [Fact]
        public void GivenTheColorIsNullWhenGetRedGreenBlueBytesFromColorIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action GetBytesFromColor(Color color)
            {
                return () => this.systemUnderTest.GetRedGreenBlueBytesFromColor(color);
            }

            // Act.
            var action = GetBytesFromColor(new Color());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = GetBytesFromColor(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="ColorComponent.GetRedGreenBlueBytesFromColor(Color)"/>.
        /// </summary>
        [Fact]
        public void GivenAColorWhenGetRedGreenBlueBytesFromColorIsCalledThenTheCorrectBytesAreReturned()
        {
            // Arrange.
            var color = new Color
            {
                RedComponent = 0D,
                GreenComponent = 0.5D,
                BlueComponent = 1D,
            };

            // Act.
            var result = this.systemUnderTest.GetRedGreenBlueBytesFromColor(color);

            // Assert.
            result.Should().BeEquivalentTo(
                new List<byte>
                {
                    0,
                    127,
                    255,
                });
        }

        /// <summary>
        /// Tests <see cref="ColorComponent.Multiply(Color, double)"/>.
        /// </summary>
        [Fact]
        public void GivenTheColorIsNullWhenMultiplyIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action Multiply(Color color)
            {
                return () => this.systemUnderTest.Multiply(color, 0D);
            }

            // Act.
            var action = Multiply(new Color());

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
        /// Tests <see cref="ColorComponent.Multiply(Color, double)"/>.
        /// </summary>
        [Fact]
        public void GivenAColorAndAFactorWhenMultiplyIsCalledThenTheProductIsReturned()
        {
            // Arrange.
            var color = new Color
            {
                RedComponent = 0D,
                GreenComponent = 0.5D,
                BlueComponent = 1D,
            };
            var factor = 0.5D;

            // Act.
            var result = this.systemUnderTest.Multiply(color, factor);

            // Assert.
            result.Should().BeEquivalentTo(
                new Color
                {
                    RedComponent = 0D,
                    GreenComponent = 0.25D,
                    BlueComponent = 0.5D,
                },
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
        }
    }
}
