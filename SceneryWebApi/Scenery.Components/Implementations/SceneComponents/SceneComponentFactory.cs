// <copyright file="SceneComponentFactory.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents
{
    using System;
    using System.Linq;
    using Scenery.Components.Interfaces;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;
    using Scenery.Models.Scenes;

    /// <inheritdoc/>
    public class SceneComponentFactory : ISceneComponentFactory
    {
        private readonly IFuncDoubleDoubleComponent funcDoubleDoubleComponent;
        private readonly ILine3Component line3Component;
        private readonly IMatrix4Component matrix4Component;
        private readonly IVector3Component vector3Component;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneComponentFactory"/> class.
        /// </summary>
        /// <param name="funcDoubleDoubleComponent">An <see cref="IFuncDoubleDoubleComponent"/>.</param>
        /// <param name="line3Component">An <see cref="ILine3Component "/>.</param>
        /// <param name="matrix4Component">An <see cref="IMatrix4Component"/>.</param>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        public SceneComponentFactory(
            IFuncDoubleDoubleComponent funcDoubleDoubleComponent,
            ILine3Component line3Component,
            IMatrix4Component matrix4Component,
            IVector3Component vector3Component)
        {
            this.funcDoubleDoubleComponent = funcDoubleDoubleComponent;
            this.line3Component = line3Component;
            this.matrix4Component = matrix4Component;
            this.vector3Component = vector3Component;
        }

        /// <inheritdoc/>
        public ISceneComponent CreateSceneComponent(Scene scene)
        {
            if (scene == null)
            {
                throw new ArgumentNullException(nameof(scene));
            }

            return scene switch
            {
                ColoredScene coloredScene => this.CreateColoredSceneComponent(coloredScene),
                ConeScene _ => this.CreateConeSceneComponent(),
                CubeScene _ => this.CreateCubeSceneComponent(),
                CylinderScene _ => this.CreateCylinderSceneComponent(),
                DodecahedronScene _ => this.CreateDodecahedronSceneComponent(),
                EmptyScene _ => CreateEmptySceneComponent(),
                FullScene _ => CreateFullSceneComponent(),
                IcosahedronScene _ => this.CreateIcosahedronSceneComponent(),
                IntersectedScene intersectedScene => this.CreateIntersectedSceneComponent(intersectedScene),
                InvertedScene invertedScene => this.CreateInvertedSceneComponent(invertedScene),
                InvisibleScene invisibleScene => this.CreateInvisibleSceneComponent(invisibleScene),
                OctahedronScene _ => this.CreateOctahedronSceneComponent(),
                PlaneScene planeScene => this.CreatePlaneSceneComponent(planeScene),
                RotatedScene rotatedScene => this.CreateRotatedSceneComponent(rotatedScene),
                ScaledScene scaledScene => this.CreateScaledSceneComponent(scaledScene),
                SphereScene _ => this.CreateSphereSceneComponent(),
                TetrahedronScene _ => this.CreateTetrahedronSceneComponent(),
                TranslatedScene translatedScene => this.CreateTranslatedSceneComponent(translatedScene),
                UnitedScene unitedScene => this.CreateUnitedSceneComponent(unitedScene),
                _ => throw new NotSupportedException($"Unknown {nameof(Scene)} {scene.GetType().Name}."),
            };
        }

        private static ISceneComponent CreateEmptySceneComponent()
        {
            return new EmptySceneComponent();
        }

        private static ISceneComponent CreateFullSceneComponent()
        {
            return new FullSceneComponent();
        }

        private ISceneComponent CreateColoredSceneComponent(ColoredScene coloredScene)
        {
            return new ColoredSceneComponent(
                this.CreateSceneComponent(coloredScene.OriginalScene),
                coloredScene.Color);
        }

        private ISceneComponent CreateConeSceneComponent()
        {
            return new ConeSceneComponent(
                this.funcDoubleDoubleComponent,
                this.line3Component,
                this.vector3Component);
        }

        private ISceneComponent CreateCubeSceneComponent()
        {
            var intersectedScene = new IntersectedScene
            {
                Scenes =
                {
                    new PlaneScene
                    {
                        Normal = new Vector3
                        {
                            XCoordinate = 0D,
                            YCoordinate = 0D,
                            ZCoordinate = -1D,
                        },
                    },
                    new PlaneScene
                    {
                        Normal = new Vector3
                        {
                            XCoordinate = 1D,
                            YCoordinate = 0D,
                            ZCoordinate = 0D,
                        },
                    },
                    new PlaneScene
                    {
                        Normal = new Vector3
                        {
                            XCoordinate = 0D,
                            YCoordinate = 1D,
                            ZCoordinate = 0D,
                        },
                    },
                    new PlaneScene
                    {
                        Normal = new Vector3
                        {
                            XCoordinate = -1D,
                            YCoordinate = 0D,
                            ZCoordinate = 0D,
                        },
                    },
                    new PlaneScene
                    {
                        Normal = new Vector3
                        {
                            XCoordinate = 0D,
                            YCoordinate = -1D,
                            ZCoordinate = 0D,
                        },
                    },
                    new PlaneScene
                    {
                        Normal = new Vector3
                        {
                            XCoordinate = 0D,
                            YCoordinate = 0D,
                            ZCoordinate = 1D,
                        },
                    },
                },
            };

            return this.CreateSceneComponent(intersectedScene);
        }

        private ISceneComponent CreateCylinderSceneComponent()
        {
            return new CylinderSceneComponent(
                this.funcDoubleDoubleComponent,
                this.line3Component,
                this.vector3Component);
        }

        private ISceneComponent CreateDodecahedronSceneComponent()
        {
            var dihedralAngle = Math.Acos(-1D / Math.Sqrt(5D));
            const double azimuthStep = Math.PI / 5D;

            var intersectedScene = new IntersectedScene
            {
                Scenes =
                {
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI, 0D),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 0D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 2D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 4D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 6D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 8D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 1D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 3D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 5D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 7D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 9D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, 0D, 0D),
                    },
                },
            };

            return this.CreateSceneComponent(intersectedScene);
        }

        private ISceneComponent CreateIcosahedronSceneComponent()
        {
            var dihedralAngle = Math.Acos(-Math.Sqrt(5D) / 3D);
            var secondInclination = Math.Acos(-1D / 3D);
            const double azimuthStep = Math.PI / 3D;
            var azimuthOffset = (Math.PI / 3D) - Math.Acos(Math.Sqrt(5D / 8D));

            var intersectedScene = new IntersectedScene
            {
                Scenes =
                {
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI, 0D),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 0D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 2D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 4D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (1D * azimuthStep) - azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (1D * azimuthStep) + azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (3D * azimuthStep) - azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (3D * azimuthStep) + azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (5D * azimuthStep) - azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (5D * azimuthStep) + azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (0D * azimuthStep) - azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (0D * azimuthStep) + azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (2D * azimuthStep) - azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (2D * azimuthStep) + azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (4D * azimuthStep) - azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (4D * azimuthStep) + azimuthOffset),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 1D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 3D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 5D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, 0D, 0D),
                    },
                },
            };

            return this.CreateSceneComponent(intersectedScene);
        }

        private ISceneComponent CreateIntersectedSceneComponent(IntersectedScene intersectedScene)
        {
            return intersectedScene.Scenes.Aggregate(
                this.CreateSceneComponent(new FullScene()),
                (originalSceneComponent, otherScene)
                    => new IntersectedSceneComponent(
                        this.line3Component,
                        originalSceneComponent,
                        this.CreateSceneComponent(otherScene)));
        }

        private ISceneComponent CreateInvertedSceneComponent(InvertedScene invertedScene)
        {
            return new InvertedSceneComponent(
                this.vector3Component,
                this.CreateSceneComponent(invertedScene.OriginalScene));
        }

        private ISceneComponent CreateInvisibleSceneComponent(InvisibleScene invisibleScene)
        {
            return new InvisibleSceneComponent(
                this.CreateSceneComponent(invisibleScene.OriginalScene));
        }

        private ISceneComponent CreateOctahedronSceneComponent()
        {
            var dihedralAngle = Math.Acos(-1D / 3D);
            const double azimuthStep = Math.PI / 3D;

            var intersectedScene = new IntersectedScene
            {
                Scenes =
                {
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI, 0D),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 0D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 2D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 4D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 1D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 3D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 5D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, 0D, 0D),
                    },
                },
            };

            return this.CreateSceneComponent(intersectedScene);
        }

        private ISceneComponent CreatePlaneSceneComponent(PlaneScene planeScene)
        {
            return new PlaneSceneComponent(
                this.vector3Component,
                planeScene.Normal);
        }

        private ISceneComponent CreateRotatedSceneComponent(RotatedScene rotatedScene)
        {
            return new AffinelyTransformedSceneComponent(
                this.matrix4Component,
                this.CreateSceneComponent(rotatedScene.OriginalScene),
                this.matrix4Component.GetRotationMatrix(rotatedScene.Axis, rotatedScene.Angle),
                this.matrix4Component.GetRotationMatrix(rotatedScene.Axis, -rotatedScene.Angle));
        }

        private ISceneComponent CreateScaledSceneComponent(ScaledScene scaledScene)
        {
            return new AffinelyTransformedSceneComponent(
                this.matrix4Component,
                this.CreateSceneComponent(scaledScene.OriginalScene),
                this.matrix4Component.GetScalingMatrix(scaledScene.Factor),
                this.matrix4Component.GetScalingMatrix(1 / scaledScene.Factor));
        }

        private ISceneComponent CreateSphereSceneComponent()
        {
            return new SphereSceneComponent(
                this.funcDoubleDoubleComponent,
                this.line3Component,
                this.vector3Component);
        }

        private ISceneComponent CreateTetrahedronSceneComponent()
        {
            var dihedralAngle = Math.Acos(1D / 3D);
            const double azimuthStep = 2D * Math.PI / 3D;

            var intersectedScene = new IntersectedScene
            {
                Scenes =
                {
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI, 0D),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 0D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 1D * azimuthStep),
                    },
                    new PlaneScene
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 2D * azimuthStep),
                    },
                },
            };

            return this.CreateSceneComponent(intersectedScene);
        }

        private ISceneComponent CreateTranslatedSceneComponent(TranslatedScene translatedScene)
        {
            return new AffinelyTransformedSceneComponent(
                this.matrix4Component,
                this.CreateSceneComponent(translatedScene.OriginalScene),
                this.matrix4Component.GetTranslationMatrix(translatedScene.Translation),
                this.matrix4Component.GetTranslationMatrix(this.vector3Component.Multiply(translatedScene.Translation, -1D)));
        }

        private ISceneComponent CreateUnitedSceneComponent(UnitedScene unitedScene)
        {
            return unitedScene.Scenes.Aggregate(
                this.CreateSceneComponent(new EmptyScene()),
                (originalSceneComponent, otherScene)
                    => new UnitedSceneComponent(
                        this.line3Component,
                        originalSceneComponent,
                        this.CreateSceneComponent(otherScene)));
        }
    }
}
