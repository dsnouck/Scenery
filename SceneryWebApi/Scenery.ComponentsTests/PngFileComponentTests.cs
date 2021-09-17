// <copyright file="PngFileComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Moq;
    using Scenery.Components.Implementations;
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="PngFileComponent"/>.
    /// </summary>
    public class PngFileComponentTests
    {
        private readonly PngFileComponent systemUnderTest;
        private readonly Mock<IBitmapComponent> bitmapComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="PngFileComponentTests"/> class.
        /// </summary>
        public PngFileComponentTests()
        {
            this.bitmapComponentTestDouble = new Mock<IBitmapComponent>();
            this.systemUnderTest = new PngFileComponent(
                this.bitmapComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="PngFileComponent.GetStream(List{List{Color}})"/>.
        /// </summary>
        [Fact]
        public void GivenABitmapWhenGetStreamIsCalledThenBitmapComponentCreateSystemDrawingBitmapIsCalled()
        {
            // Arrange.
            var bitmap = new List<List<Color>>
            {
                new List<Color>
                {
                    new Color(),
                },
            };
            var systemDrawingBitmapTestDouble = new System.Drawing.Bitmap(1, 1);
            this.bitmapComponentTestDouble
                .Setup(component => component.CreateSystemDrawingBitmap(It.IsAny<List<List<Color>>>()))
                .Returns(systemDrawingBitmapTestDouble);

            // Act.
            var result = this.systemUnderTest.GetStream(bitmap);

            // Assert.
            result.Should().NotBeNull();
            this.bitmapComponentTestDouble
                .Verify(component => component.CreateSystemDrawingBitmap(It.IsAny<List<List<Color>>>()), Times.Once);
        }
    }
}
