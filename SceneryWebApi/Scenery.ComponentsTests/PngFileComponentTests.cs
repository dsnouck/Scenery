// <copyright file="PngFileComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests
{
    using Moq;
    using Scenery.Components.Implementations;
    using Scenery.Components.Interfaces;

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
    }
}
