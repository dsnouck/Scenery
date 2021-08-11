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
            Action CreateSceneComponent(Scene scene)
            {
                return () => this.systemUnderTest.CreateSceneComponent(scene);
            }

            // Act.
            var action = CreateSceneComponent(new SphereScene());

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = CreateSceneComponent(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenANotImplementedSceneWhenCreateSceneComponentIsCalledThenANotSupportedExceptionIsThrown()
        {
            // Arrange.
            Action CreateSceneComponent(Scene scene)
            {
                return () => this.systemUnderTest.CreateSceneComponent(scene);
            }

            // Act.
            var action = CreateSceneComponent(new SphereScene());

            // Assert.
            action.Should().NotThrow();

            // Act.
            action = CreateSceneComponent(new Scene());

            // Assert.
            action.Should().Throw<NotSupportedException>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAColoredSceneWhenCreateSceneComponentIsCalledThenAColoredSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new ColoredScene
            {
                Color = new Color(),
                OriginalScene = new SphereScene(),
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<ColoredSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAConeSceneWhenCreateSceneComponentIsCalledThenAConeSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new ConeScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<ConeSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenACubeSceneWhenCreateSceneComponentIsCalledThenAnIntersectedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new CubeScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectedSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenACylinderSceneWhenCreateSceneComponentIsCalledThenACylinderSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new CylinderScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<CylinderSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenADodecahedronSceneWhenCreateSceneComponentIsCalledThenAnIntersectedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new DodecahedronScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectedSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAnEmptySceneWhenCreateSceneComponentIsCalledThenAnEmptySceneComponentIsReturned()
        {
            // Arrange.
            var scene = new EmptyScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<EmptySceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAFullSceneWhenCreateSceneComponentIsCalledThenAFullSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new FullScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<FullSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAnIcosahedronSceneWhenCreateSceneComponentIsCalledThenAnIntersectedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new IcosahedronScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectedSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAnIntersectedSceneWhenCreateSceneComponentIsCalledThenAnIntersectedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new IntersectedScene
            {
                Scenes =
                {
                    new CubeScene(),
                    new SphereScene(),
                },
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectedSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAnInvisibleSceneWhenCreateSceneComponentIsCalledThenAnInvisibleSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new InvisibleScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<InvisibleSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAnInvertedSceneWhenCreateSceneComponentIsCalledThenAnInvertedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new InvertedScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<InvertedSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAOctahedronSceneWhenCreateSceneComponentIsCalledThenAnIntersectedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new OctahedronScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectedSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAPlaneSceneWhenCreateSceneComponentIsCalledThenAPlaneSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new PlaneScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<PlaneSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenARotatedSceneWhenCreateSceneComponentIsCalledThenAnAffinelyTransformedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new RotatedScene
            {
                Axis = new Vector3(),
                Angle = Math.PI / 4D,
                OriginalScene = new SphereScene(),
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<AffinelyTransformedSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAScaledSceneWhenCreateSceneComponentIsCalledThenAnAffinelyTransformedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new ScaledScene
            {
                Factor = 0.5D,
                OriginalScene = new SphereScene(),
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<AffinelyTransformedSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenASphereSceneWhenCreateSceneComponentIsCalledThenASphereSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new SphereScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<SphereSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenATetrahedronSceneWhenCreateSceneComponentIsCalledThenAnIntersectedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new TetrahedronScene();

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<IntersectedSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenATranslatedSceneWhenCreateSceneComponentIsCalledThenAnAffinelyTransformedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new TranslatedScene
            {
                Translation = new Vector3(),
                OriginalScene = new SphereScene(),
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<AffinelyTransformedSceneComponent>();
        }

        /// <summary>
        /// Tests <see cref="SceneComponentFactory.CreateSceneComponent(Scene)"/>.
        /// </summary>
        [Fact]
        public void GivenAUnitedSceneWhenCreateSceneComponentIsCalledThenAUnitedSceneComponentIsReturned()
        {
            // Arrange.
            var scene = new UnitedScene
            {
                Scenes =
                {
                    new CubeScene(),
                    new SphereScene(),
                },
            };

            // Act.
            var result = this.systemUnderTest.CreateSceneComponent(scene);

            // Assert.
            result.Should().BeOfType<UnitedSceneComponent>();
        }
    }
}
