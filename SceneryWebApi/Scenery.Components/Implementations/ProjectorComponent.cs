// <copyright file="ProjectorComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations;

using Scenery.Components.Interfaces;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;
using Scenery.Models.Scenes;

/// <inheritdoc/>
public class ProjectorComponent : IProjectorComponent
{
    private readonly IColorComponent colorComponent;
    private readonly IFuncVector2Vector3Component funcVector2Vector3Component;
    private readonly ISceneComponentFactory sceneComponentFactory;
    private readonly IVector3Component vector3Component;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectorComponent"/> class.
    /// </summary>
    /// <param name="colorComponent">An <see cref="IColorComponent"/>.</param>
    /// <param name="funcVector2Vector3Component">An <see cref="IFuncVector2Vector3Component"/>.</param>
    /// <param name="sceneComponentFactory">An <see cref="ISceneComponentFactory"/>.</param>
    /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
    public ProjectorComponent(
        IColorComponent colorComponent,
        IFuncVector2Vector3Component funcVector2Vector3Component,
        ISceneComponentFactory sceneComponentFactory,
        IVector3Component vector3Component)
    {
        this.colorComponent = colorComponent;
        this.funcVector2Vector3Component = funcVector2Vector3Component;
        this.sceneComponentFactory = sceneComponentFactory;
        this.vector3Component = vector3Component;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This implementation is intentionally simple.
    /// A screen is suspended between the eye and the focus.
    /// The image is projected on that screen using perspective projection.
    /// The only lighting is a single point source at the eye.
    /// The colors in the image only depend on the color of the scene and the angle between the line of sight and the surface.
    /// They do not depend on the distance between the eye and the scene.
    /// </remarks>
    public Func<Vector2, Color> ProjectSceneToImage(Scene scene, ProjectorSettings projectorSettings)
    {
        ArgumentNullException.ThrowIfNull(projectorSettings);

        var screen = this.GetScreen(projectorSettings);
        var sceneComponent = this.sceneComponentFactory.CreateSceneComponent(scene);

        return point =>
        {
            var direction = this.vector3Component.Normalize(
                this.vector3Component.Subtract(
                    screen(point),
                    projectorSettings.Eye));
            var lineOfSight = new Line3
            {
                Origin = projectorSettings.Eye,
                Direction = direction,
            };
            var firstOrDefaultSurfaceIntersection = sceneComponent.GetAllSurfaceIntersections(lineOfSight)
                .Where(surfaceIntersection => surfaceIntersection.Distance > 0D)
                .OrderBy(surfaceIntersection => surfaceIntersection.Distance)
                .FirstOrDefault();
            if (firstOrDefaultSurfaceIntersection == null)
            {
                return projectorSettings.Background;
            }

            var intensity = Math.Abs(this.vector3Component.DotProduct(firstOrDefaultSurfaceIntersection.Normal(), direction));
            return this.colorComponent.Multiply(firstOrDefaultSurfaceIntersection.Color, intensity);
        };
    }

    private Func<Vector2, Vector3> GetScreen(ProjectorSettings projectorSettings)
    {
        var viewingDirection = this.vector3Component.Normalize(
            this.vector3Component.Subtract(projectorSettings.Focus, projectorSettings.Eye));
        var centerScreen = this.vector3Component.Add(projectorSettings.Eye, viewingDirection);
        var vertical = new Vector3 { X = 0D, Y = 0D, Z = 1D };
        var xVector = this.vector3Component.Normalize(
            this.vector3Component.CrossProduct(
                viewingDirection,
                vertical));
        var yVector = this.vector3Component.Normalize(
                this.vector3Component.CrossProduct(
                    xVector,
                    viewingDirection));
        var halfScreenExtent = Math.Tan(projectorSettings.FieldOfView * 0.5D);
        xVector = this.vector3Component.Multiply(xVector, halfScreenExtent);
        yVector = this.vector3Component.Multiply(yVector, halfScreenExtent);
        return this.funcVector2Vector3Component.CreatePlane(centerScreen, xVector, yVector);
    }
}
