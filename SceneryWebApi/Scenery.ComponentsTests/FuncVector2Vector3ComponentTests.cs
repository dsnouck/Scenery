// <copyright file="FuncVector2Vector3ComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests
{
    using Moq;
    using Scenery.Components.Implementations;
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="FuncVector2Vector3Component"/>.
    /// </summary>
    public class FuncVector2Vector3ComponentTests
    {
        private readonly FuncVector2Vector3Component systemUnderTest;
        private readonly Mock<IVector3Component> vector3ComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncVector2Vector3ComponentTests"/> class.
        /// </summary>
        public FuncVector2Vector3ComponentTests()
        {
            this.vector3ComponentTestDouble = new Mock<IVector3Component>();
            this.systemUnderTest = new FuncVector2Vector3Component(
                this.vector3ComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="FuncVector2Vector3Component.GetPlane(Vector3, Vector3, Vector3)"/>.
        /// </summary>
        [Fact]
        public void GivenAnOriginAndTwoDirectionsWhenGetPlaneIsCalledThenTheCorrectPlaneIsReturned()
        {
            // Arrange.
            var origin = new Vector3();
            var xDirection = new Vector3();
            var yDirection = new Vector3();

            // Act.
            var result = this.systemUnderTest.GetPlane(origin, xDirection, yDirection);

            // Assert.
            result(new Vector2());
            this.vector3ComponentTestDouble
                .Verify(component => component.Add(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Exactly(2));
            this.vector3ComponentTestDouble
                .Verify(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()), Times.Exactly(2));
        }
    }
}
