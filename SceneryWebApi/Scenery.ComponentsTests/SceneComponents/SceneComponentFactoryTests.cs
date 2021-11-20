// <copyright file="SceneComponentFactoryTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponents
{
    using System;
    using FluentAssertions;
    using Moq;
    using Scenery.Components.Implementations.SceneComponents;
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using Scenery.Models.Scenes;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="SceneComponentFactory"/>.
    /// </summary>
    public class SceneComponentFactoryTests
    {
        private readonly SceneComponentFactory systemUnderTest;
        private readonly Mock<IFuncDoubleDoubleComponent> funcDoubleDoubleComponentTestDouble;
        private readonly Mock<ILine3Component> line3ComponentTestDouble;
        private readonly Mock<IMatrix4Component> matrix4ComponentTestDouble;
        private readonly Mock<IVector3Component> vector3ComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneComponentFactoryTests"/> class.
        /// </summary>
        public SceneComponentFactoryTests()
        {
            this.funcDoubleDoubleComponentTestDouble = new Mock<IFuncDoubleDoubleComponent>();
            this.line3ComponentTestDouble = new Mock<ILine3Component>();
            this.matrix4ComponentTestDouble = new Mock<IMatrix4Component>();
            this.vector3ComponentTestDouble = new Mock<IVector3Component>();
            this.systemUnderTest = new SceneComponentFactory(
                this.funcDoubleDoubleComponentTestDouble.Object,
                this.line3ComponentTestDouble.Object,
                this.matrix4ComponentTestDouble.Object,
                this.vector3ComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenTheSceneIsNullWhenCreateSceneComponentIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Scene scene = null;

            // Act.
            Action action = () => this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenANonspecificSceneWhenCreateSceneComponentIsCalledThenANotSupportedExceptionIsThrown()
        {
            // Arrange.
            var scene = new Scene();

            // Act.
            Action action = () => this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            action.Should().Throw<NotSupportedException>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAColoredWhenCreateSceneComponentIsCalledThenAColoredComponentIsReturned()
        {
            // Arrange.
            var scene = new Colored
            {
                Color = new Color(),
                Scene = new Sphere(),
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<ColoredComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAConeWhenCreateSceneComponentIsCalledThenAConeComponentIsReturned()
        {
            // Arrange.
            var scene = new Cone();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<ConeComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenACubeWhenCreateSceneComponentIsCalledThenAnIntersectionComponentIsReturned()
        {
            // Arrange.
            var scene = new Cube();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectionComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenACylinderWhenCreateSceneComponentIsCalledThenACylinderComponentIsReturned()
        {
            // Arrange.
            var scene = new Cylinder();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<CylinderComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenADodecahedronWhenCreateSceneComponentIsCalledThenAnIntersectionComponentIsReturned()
        {
            // Arrange.
            var scene = new Dodecahedron();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectionComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAnEmptyWhenCreateSceneComponentIsCalledThenAnEmptyComponentIsReturned()
        {
            // Arrange.
            var scene = new Empty();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<EmptyComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAFullWhenCreateSceneComponentIsCalledThenAFullComponentIsReturned()
        {
            // Arrange.
            var scene = new Full();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<FullComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAnIcosahedronWhenCreateSceneComponentIsCalledThenAnIntersectionComponentIsReturned()
        {
            // Arrange.
            var scene = new Icosahedron();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectionComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAnIntersectionWhenCreateSceneComponentIsCalledThenAnIntersectionComponentIsReturned()
        {
            // Arrange.
            var scene = new Intersection
            {
                Scenes =
                {
                    new Cube(),
                    new Sphere(),
                },
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectionComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAnTransparentWhenCreateSceneComponentIsCalledThenAnTransparentComponentIsReturned()
        {
            // Arrange.
            var scene = new Transparent();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<TransparentComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAnInvertedWhenCreateSceneComponentIsCalledThenAnInvertedComponentIsReturned()
        {
            // Arrange.
            var scene = new Inverted();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<InvertedComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAOctahedronWhenCreateSceneComponentIsCalledThenAnIntersectionComponentIsReturned()
        {
            // Arrange.
            var scene = new Octahedron();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectionComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAPlaneWhenCreateSceneComponentIsCalledThenAPlaneComponentIsReturned()
        {
            // Arrange.
            var scene = new Plane();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<PlaneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenARotatedWhenCreateSceneComponentIsCalledThenAnAffinelyTransformedComponentIsReturned()
        {
            // Arrange.
            var scene = new Rotated
            {
                Axis = new Vector3(),
                Angle = Math.PI / 4D,
                Scene = new Sphere(),
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<AffinelyTransformedComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAScaledWhenCreateSceneComponentIsCalledThenAnAffinelyTransformedComponentIsReturned()
        {
            // Arrange.
            var scene = new Scaled
            {
                Factor = 0.5D,
                Scene = new Sphere(),
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<AffinelyTransformedComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenASphereWhenCreateSceneComponentIsCalledThenASphereComponentIsReturned()
        {
            // Arrange.
            var scene = new Sphere();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<SphereComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenATetrahedronWhenCreateSceneComponentIsCalledThenAnIntersectionComponentIsReturned()
        {
            // Arrange.
            var scene = new Tetrahedron();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectionComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenATranslatedWhenCreateSceneComponentIsCalledThenAnAffinelyTransformedComponentIsReturned()
        {
            // Arrange.
            var scene = new Translated
            {
                Translation = new Vector3(),
                Scene = new Sphere(),
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<AffinelyTransformedComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAUnionWhenCreateSceneComponentIsCalledThenAUnionComponentIsReturned()
        {
            // Arrange.
            var scene = new Union
            {
                Scenes =
                {
                    new Cube(),
                    new Sphere(),
                },
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<UnionComponent>();
        }
    }
}
