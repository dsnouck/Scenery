// <copyright file="ProjectorComponent.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using Scenery.Components.Interfaces;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;
    using Scenery.Models.Scenes;
    using System;
    using System.Linq;

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
        public Func<Vector2, Color> ProjectSceneToImage(Scene scene, ProjectorSettings projectorSettings)
        {
            if (projectorSettings == null)
            {
                throw new ArgumentNullException(nameof(projectorSettings));
            }

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
                var firstOrDefaultIntercept = sceneComponent.GetAllIntercepts(lineOfSight)
                    .Where(intercept => intercept.Distance > 0D)
                    .OrderBy(intercept => intercept.Distance)
                    .FirstOrDefault();
                if (firstOrDefaultIntercept == null)
                {
                    return projectorSettings.BackgroundColor;
                }

                var intensity = Math.Abs(this.vector3Component.DotProduct(firstOrDefaultIntercept.Normal(), direction));
                return this.colorComponent.Multiply(firstOrDefaultIntercept.Color, intensity);
            };
        }

        private Func<Vector2, Vector3> GetScreen(ProjectorSettings projectorSettings)
        {
            var viewingDirection = this.vector3Component.Normalize(
                this.vector3Component.Subtract(projectorSettings.Focus, projectorSettings.Eye));
            var centerScreen = this.vector3Component.Add(projectorSettings.Eye, viewingDirection);
            var vertical = new Vector3 { XCoordinate = 0D, YCoordinate = 0D, ZCoordinate = 1D };
            var xVector = this.vector3Component.Normalize(
                this.vector3Component.CrossProduct(
                    viewingDirection,
                    vertical));
            var yVector = this.vector3Component.Normalize(
                    this.vector3Component.CrossProduct(
                        xVector,
                        viewingDirection));
            var halfScreenExtent = Math.Tan(projectorSettings.HorizontalOpeningAngle * 0.5D);
            xVector = this.vector3Component.Multiply(xVector, halfScreenExtent);
            yVector = this.vector3Component.Multiply(yVector, halfScreenExtent);
            return this.funcVector2Vector3Component.GetPlane(centerScreen, xVector, yVector);
        }
    }
}
