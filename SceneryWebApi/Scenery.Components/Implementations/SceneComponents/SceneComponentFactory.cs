// <copyright file="SceneComponentFactory.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents;

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
        ArgumentNullException.ThrowIfNull(scene);

        return scene switch
        {
            Colored colored => this.CreateColoredComponent(colored),
            Cone _ => this.CreateConeComponent(),
            Cube _ => this.CreateCubeComponent(),
            Cylinder _ => this.CreateCylinderComponent(),
            Dodecahedron _ => this.CreateDodecahedronComponent(),
            Empty _ => CreateEmptyComponent(),
            Full _ => CreateFullComponent(),
            Icosahedron _ => this.CreateIcosahedronComponent(),
            Intersection intersection => this.CreateIntersectionComponent(intersection),
            Inverted inverted => this.CreateInvertedComponent(inverted),
            Transparent transparent => this.CreateTransparentComponent(transparent),
            Octahedron _ => this.CreateOctahedronComponent(),
            Plane plane => this.CreatePlaneComponent(plane),
            Rotated rotated => this.CreateRotatedComponent(rotated),
            Scaled scaled => this.CreateScaledComponent(scaled),
            Sphere _ => this.CreateSphereComponent(),
            Tetrahedron _ => this.CreateTetrahedronComponent(),
            Translated translated => this.CreateTranslatedComponent(translated),
            Union union => this.CreateUnionComponent(union),
            _ => throw new NotSupportedException($"Unknown {nameof(Scene)} {scene.GetType().Name}."),
        };
    }

    private static ISceneComponent CreateEmptyComponent()
    {
        return new EmptyComponent();
    }

    private static ISceneComponent CreateFullComponent()
    {
        return new FullComponent();
    }

    private ISceneComponent CreateColoredComponent(Colored colored)
    {
        return new ColoredComponent(
            this.CreateSceneComponent(colored.Scene),
            colored.Color);
    }

    private ISceneComponent CreateConeComponent()
    {
        return new ConeComponent(
            this.funcDoubleDoubleComponent,
            this.line3Component,
            this.vector3Component);
    }

    private ISceneComponent CreateCubeComponent()
    {
        var intersection = new Intersection
        {
            Scenes =
                {
                    new Plane
                    {
                        Normal = new Vector3
                        {
                            X = 0D,
                            Y = 0D,
                            Z = -1D,
                        },
                    },
                    new Plane
                    {
                        Normal = new Vector3
                        {
                            X = 1D,
                            Y = 0D,
                            Z = 0D,
                        },
                    },
                    new Plane
                    {
                        Normal = new Vector3
                        {
                            X = 0D,
                            Y = 1D,
                            Z = 0D,
                        },
                    },
                    new Plane
                    {
                        Normal = new Vector3
                        {
                            X = -1D,
                            Y = 0D,
                            Z = 0D,
                        },
                    },
                    new Plane
                    {
                        Normal = new Vector3
                        {
                            X = 0D,
                            Y = -1D,
                            Z = 0D,
                        },
                    },
                    new Plane
                    {
                        Normal = new Vector3
                        {
                            X = 0D,
                            Y = 0D,
                            Z = 1D,
                        },
                    },
                },
        };

        return this.CreateSceneComponent(intersection);
    }

    private ISceneComponent CreateCylinderComponent()
    {
        return new CylinderComponent(
            this.funcDoubleDoubleComponent,
            this.line3Component,
            this.vector3Component);
    }

    private ISceneComponent CreateDodecahedronComponent()
    {
        var dihedralAngle = Math.Acos(-1D / Math.Sqrt(5D));
        const double azimuthStep = Math.PI / 5D;

        var intersection = new Intersection
        {
            Scenes =
                {
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI, 0D),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 0D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 2D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 4D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 6D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 8D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 1D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 3D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 5D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 7D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 9D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, 0D, 0D),
                    },
                },
        };

        return this.CreateSceneComponent(intersection);
    }

    private ISceneComponent CreateIcosahedronComponent()
    {
        var dihedralAngle = Math.Acos(-Math.Sqrt(5D) / 3D);
        var secondInclination = Math.Acos(-1D / 3D);
        const double azimuthStep = Math.PI / 3D;
        var azimuthOffset = (Math.PI / 3D) - Math.Acos(Math.Sqrt(5D / 8D));

        var intersection = new Intersection
        {
            Scenes =
                {
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI, 0D),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 0D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 2D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 4D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (1D * azimuthStep) - azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (1D * azimuthStep) + azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (3D * azimuthStep) - azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (3D * azimuthStep) + azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (5D * azimuthStep) - azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, secondInclination, (5D * azimuthStep) + azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (0D * azimuthStep) - azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (0D * azimuthStep) + azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (2D * azimuthStep) - azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (2D * azimuthStep) + azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (4D * azimuthStep) - azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - secondInclination, (4D * azimuthStep) + azimuthOffset),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 1D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 3D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 5D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, 0D, 0D),
                    },
                },
        };

        return this.CreateSceneComponent(intersection);
    }

    private ISceneComponent CreateIntersectionComponent(Intersection intersection)
    {
        return intersection.Scenes.Aggregate(
            this.CreateSceneComponent(new Full()),
            (sceneComponent, otherScene)
                => new IntersectionComponent(
                    this.line3Component,
                    sceneComponent,
                    this.CreateSceneComponent(otherScene)));
    }

    private ISceneComponent CreateInvertedComponent(Inverted inverted)
    {
        return new InvertedComponent(
            this.vector3Component,
            this.CreateSceneComponent(inverted.Scene));
    }

    private ISceneComponent CreateTransparentComponent(Transparent transparent)
    {
        return new TransparentComponent(
            this.CreateSceneComponent(transparent.Scene));
    }

    private ISceneComponent CreateOctahedronComponent()
    {
        var dihedralAngle = Math.Acos(-1D / 3D);
        const double azimuthStep = Math.PI / 3D;

        var intersection = new Intersection
        {
            Scenes =
                {
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI, 0D),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 0D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 2D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 4D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 1D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 3D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI - dihedralAngle, 5D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, 0D, 0D),
                    },
                },
        };

        return this.CreateSceneComponent(intersection);
    }

    private ISceneComponent CreatePlaneComponent(Plane plane)
    {
        return new PlaneComponent(
            this.vector3Component,
            plane.Normal);
    }

    private ISceneComponent CreateRotatedComponent(Rotated rotated)
    {
        return new AffinelyTransformedComponent(
            this.matrix4Component,
            this.CreateSceneComponent(rotated.Scene),
            this.matrix4Component.GetRotationMatrix(rotated.Axis, rotated.Angle),
            this.matrix4Component.GetRotationMatrix(rotated.Axis, -rotated.Angle));
    }

    private ISceneComponent CreateScaledComponent(Scaled scaled)
    {
        return new AffinelyTransformedComponent(
            this.matrix4Component,
            this.CreateSceneComponent(scaled.Scene),
            this.matrix4Component.GetScalingMatrix(scaled.Factor),
            this.matrix4Component.GetScalingMatrix(1 / scaled.Factor));
    }

    private ISceneComponent CreateSphereComponent()
    {
        return new SphereComponent(
            this.funcDoubleDoubleComponent,
            this.line3Component,
            this.vector3Component);
    }

    private ISceneComponent CreateTetrahedronComponent()
    {
        var dihedralAngle = Math.Acos(1D / 3D);
        const double azimuthStep = 2D * Math.PI / 3D;

        var intersection = new Intersection
        {
            Scenes =
                {
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, Math.PI, 0D),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 0D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 1D * azimuthStep),
                    },
                    new Plane
                    {
                        Normal = this.vector3Component.CreateVector3FromSphericalCoordinates(1D, dihedralAngle, 2D * azimuthStep),
                    },
                },
        };

        return this.CreateSceneComponent(intersection);
    }

    private ISceneComponent CreateTranslatedComponent(Translated translated)
    {
        return new AffinelyTransformedComponent(
            this.matrix4Component,
            this.CreateSceneComponent(translated.Scene),
            this.matrix4Component.GetTranslationMatrix(translated.Translation),
            this.matrix4Component.GetTranslationMatrix(this.vector3Component.Multiply(translated.Translation, -1D)));
    }

    private ISceneComponent CreateUnionComponent(Union union)
    {
        return union.Scenes.Aggregate(
            this.CreateSceneComponent(new Empty()),
            (sceneComponent, otherScene)
                => new UnionComponent(
                    this.line3Component,
                    sceneComponent,
                    this.CreateSceneComponent(otherScene)));
    }
}
