// <copyright file="SceneContainerComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations;

using Scenery.Components.Interfaces;
using Scenery.Models;
using Scenery.Models.Scenes;

/// <inheritdoc/>
public class SceneContainerComponent : ISceneContainerComponent
{
    private readonly IBitmapFileComponent bitmapFileComponent;
    private readonly IProjectorComponent projectorComponent;
    private readonly ISamplerComponent samplerComponent;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneContainerComponent"/> class.
    /// </summary>
    /// <param name="bitmapFileComponent">An <see cref="IBitmapFileComponent"/>.</param>
    /// <param name="projectorComponent">An <see cref="IProjectorComponent"/>.</param>
    /// <param name="samplerComponent">An <see cref="ISamplerComponent"/>.</param>
    public SceneContainerComponent(
        IBitmapFileComponent bitmapFileComponent,
        IProjectorComponent projectorComponent,
        ISamplerComponent samplerComponent)
    {
        this.bitmapFileComponent = bitmapFileComponent;
        this.projectorComponent = projectorComponent;
        this.samplerComponent = samplerComponent;
    }

    /// <inheritdoc/>
    public Dictionary<string, SceneContainer> GetExamples()
    {
        // Settings that nicely project a unit sphere.
        var projector = new ProjectorSettings
        {
            Eye = new Vector3
            {
                X = 2.1D,
                Y = 2.7D,
                Z = 2D,
            },
            Focus = new Vector3
            {
                X = 0D,
                Y = 0D,
                Z = 0D,
            },
            FieldOfView = Math.PI / 4D,
            Background = new Color
            {
                R = 0D,
                G = 0D,
                B = 0D,
            },
        };

        var sampler = new SamplerSettings
        {
            Columns = 160,
            Rows = 120,
            Subsamples = 2,
        };

        return new Dictionary<string, SceneContainer>
        {
            ["coloredCube"] = new SceneContainer
            {
                Scene = new Colored
                {
                    Color = new Color
                    {
                        R = 1D,
                        G = 0D,
                        B = 0D,
                    },
                    Scene = new Scaled
                    {
                        // Make the circumradius 1.
                        Factor = 1D / Math.Sqrt(3D),
                        Scene = new Cube(),
                    },
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["cone"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // Make the radii of the top and the bottom plane 0.9 the midradius of the cube.
                    Factor = 0.9D * Math.Sqrt(2D / 3D),
                    Scene = new Intersection
                    {
                        Scenes = new List<Scene>
                            {
                                new Cone(),
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
                                        X = 0D,
                                        Y = 0D,
                                        Z = 1D,
                                    },
                                },
                            },
                    },
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["cube"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // Make the circumradius 1.
                    Factor = 1D / Math.Sqrt(3D),
                    Scene = new Cube(),
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["cubeExceptSphere"] = new SceneContainer
            {
                Scene = new Intersection
                {
                    Scenes = new List<Scene>
                        {
                            new Scaled
                            {
                                // Make the circumradius 1.
                                Factor = 1D / Math.Sqrt(3D),
                                Scene = new Cube(),
                            },
                            new Inverted
                            {
                                Scene = new Scaled
                                {
                                    // Make the radius 0.9 times the midradius of the cube.
                                    Factor = 0.9D * Math.Sqrt(2D / 3D),
                                    Scene = new Colored
                                    {
                                        Color = new Color
                                        {
                                            R = 1D,
                                            G = 0D,
                                            B = 0D,
                                        },
                                        Scene = new Sphere(),
                                    },
                                },
                            },
                        },
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["cubeSphereIntersection"] = new SceneContainer
            {
                Scene = new Intersection
                {
                    Scenes = new List<Scene>
                        {
                            new Scaled
                            {
                                // Make the circumradius 1.
                                Factor = 1D / Math.Sqrt(3D),
                                Scene = new Cube(),
                            },
                            new Scaled
                            {
                                // Make the radius 0.9 times the midradius of the cube.
                                Factor = 0.9D * Math.Sqrt(2D / 3D),
                                Scene = new Colored
                                {
                                    Color = new Color
                                    {
                                        R = 1D,
                                        G = 0D,
                                        B = 0D,
                                    },
                                    Scene = new Sphere(),
                                },
                            },
                        },
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["cubeSphereUnion"] = new SceneContainer
            {
                Scene = new Union
                {
                    Scenes = new List<Scene>
                        {
                            new Scaled
                            {
                                // Make the circumradius 1.
                                Factor = 1D / Math.Sqrt(3D),
                                Scene = new Cube(),
                            },
                            new Scaled
                            {
                                // Make the radius 0.9 times the midradius of the cube.
                                Factor = 0.9D * Math.Sqrt(2D / 3D),
                                Scene = new Colored
                                {
                                    Color = new Color
                                    {
                                        R = 1D,
                                        G = 0D,
                                        B = 0D,
                                    },
                                    Scene = new Sphere(),
                                },
                            },
                        },
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["cylinder"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // Make the radius 0.9 times the midradius of the cube.
                    Factor = 0.9D * Math.Sqrt(2D / 3D),
                    Scene = new Intersection
                    {
                        Scenes = new List<Scene>
                            {
                                new Cylinder(),
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
                                        X = 0D,
                                        Y = 0D,
                                        Z = 1D,
                                    },
                                },
                            },
                    },
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["dodecahedron"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // Make the circumradius 1.
                    Factor = (1D + Math.Sqrt(5D)) / Math.Sqrt(6D * (5D - Math.Sqrt(5D))),
                    Scene = new Dodecahedron(),
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["empty"] = new SceneContainer
            {
                Scene = new Empty(),
                Projector = projector,
                Sampler = sampler,
            },
            ["full"] = new SceneContainer
            {
                Scene = new Full(),
                Projector = projector,
                Sampler = sampler,
            },
            ["icosahedron"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // Make the circumradius 1.
                    Factor = (1D + Math.Sqrt(5D)) / Math.Sqrt(6D * (5D - Math.Sqrt(5D))),
                    Scene = new Icosahedron(),
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["octahedron"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // Make the circumradius 1.
                    Factor = 1D / Math.Sqrt(3D),
                    Scene = new Octahedron(),
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["plane"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // This is the bottom plane of the cube.
                    Factor = 1D / Math.Sqrt(3D),
                    Scene = new Intersection
                    {
                        Scenes = new List<Scene>
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
                                new Transparent
                                {
                                    Scene = new Intersection
                                    {
                                        Scenes = new List<Scene>
                                        {
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
                                        },
                                    },
                                },
                            },
                    },
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["rotatedCube"] = new SceneContainer
            {
                Scene = new Rotated
                {
                    Angle = Math.PI / 4D,
                    Scene = new Scaled
                    {
                        // Make the circumradius 1.
                        Factor = 1D / Math.Sqrt(3D),
                        Scene = new Cube(),
                    },
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["scaledCube"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // Make the circumradius 1 / 2.
                    Factor = 1D / (2D * Math.Sqrt(3D)),
                    Scene = new Cube(),
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["sphere"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // Make the radius 0.9 times the midradius of the cube.
                    Factor = 0.9D * Math.Sqrt(2D / 3D),
                    Scene = new Sphere(),
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["sphereExceptCube"] = new SceneContainer
            {
                Scene = new Intersection
                {
                    Scenes = new List<Scene>
                        {
                            new Inverted
                            {
                                Scene = new Scaled
                                {
                                    // Make the circumradius 1.
                                    Factor = 1D / Math.Sqrt(3D),
                                    Scene = new Cube(),
                                },
                            },
                            new Scaled
                            {
                                // Make the radius 0.9 times the midradius of the cube.
                                Factor = 0.9D * Math.Sqrt(2D / 3D),
                                Scene = new Colored
                                {
                                    Color = new Color
                                    {
                                        R = 1D,
                                        G = 0D,
                                        B = 0D,
                                    },
                                    Scene = new Sphere(),
                                },
                            },
                        },
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["tetrahedron"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // Make the circumradius 1.
                    Factor = 1D / 3D,
                    Scene = new Tetrahedron(),
                },
                Projector = projector,
                Sampler = sampler,
            },
            ["translatedCube"] = new SceneContainer
            {
                Scene = new Scaled
                {
                    // Make the circumradius 1.
                    Factor = 1D / Math.Sqrt(3D),
                    Scene = new Translated
                    {
                        Translation = new Vector3
                        {
                            X = 0D,
                            Y = -2D,
                            Z = 0D,
                        },
                        Scene = new Cube(),
                    },
                },
                Projector = projector,
                Sampler = sampler,
            },
        };
    }

    /// <inheritdoc/>
    public Stream GetImage(SceneContainer sceneContainer)
    {
        var image = this.projectorComponent.ProjectSceneToImage(sceneContainer.Scene, sceneContainer.Projector);
        var bitmap = this.samplerComponent.SampleImageToBitmap(image, sceneContainer.Sampler);
        return this.bitmapFileComponent.GetBitmapFile(bitmap);
    }
}
