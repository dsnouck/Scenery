// <copyright file="EmptySceneComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponentTests
{
    using FluentAssertions;
    using Scenery.Components.Implementations.SceneComponents;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="EmptySceneComponent"/>.
    /// </summary>
    public class EmptySceneComponentTests
    {
        private readonly EmptySceneComponent systemUnderTest;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptySceneComponentTests"/> class.
        /// </summary>
        public EmptySceneComponentTests()
        {
            this.systemUnderTest = new EmptySceneComponent();
        }

        /// <summary>
        /// Tests <see cref="EmptySceneComponent.Contains(Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAPointWhenContainsIsCalledThenFalseIsReturned()
        {
            // Arrange.
            var point = new Vector3();

            // Act.
            var result = this.systemUnderTest.Contains(point);

            // Assert.
            result.Should().BeFalse();
        }

        /// <summary>
        /// Tests <see cref="EmptySceneComponent.GetAllIntercepts(Line3)"/>.
        /// </summary>
        [Fact]
        public void GivenALineOfSightWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
        {
            // Arrange.
            var lineOfSight = new Line3();

            // Act.
            var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

            // Assert.
            result.Should().BeEmpty();
        }
    }
}
