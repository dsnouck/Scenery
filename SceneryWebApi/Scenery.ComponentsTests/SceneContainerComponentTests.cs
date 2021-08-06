// <copyright file="SceneContainerComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests
{
    using Moq;
    using Scenery.Components.Implementations;
    using Scenery.Components.Interfaces;

    /// <summary>
    /// Provides tests for <see cref="SceneContainerComponent"/>.
    /// </summary>
    public class SceneContainerComponentTests
    {
        private readonly SceneContainerComponent systemUnderTest;
        private readonly Mock<IBitmapFileComponent> bitmapFileComponentTestDouble;
        private readonly Mock<IProjectorComponent> projectorComponentTestDouble;
        private readonly Mock<ISamplerComponent> samplerComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneContainerComponentTests"/> class.
        /// </summary>
        public SceneContainerComponentTests()
        {
            this.bitmapFileComponentTestDouble = new Mock<IBitmapFileComponent>();
            this.projectorComponentTestDouble = new Mock<IProjectorComponent>();
            this.samplerComponentTestDouble = new Mock<ISamplerComponent>();
            this.systemUnderTest = new SceneContainerComponent(
                this.bitmapFileComponentTestDouble.Object,
                this.projectorComponentTestDouble.Object,
                this.samplerComponentTestDouble.Object);
        }
    }
}
