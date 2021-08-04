// <copyright file="InvertedSceneComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents
{
    using System.Collections.Generic;
    using System.Linq;
    using Scenery.Components.Interfaces;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;

    /// <inheritdoc/>
    public class InvertedSceneComponent : ISceneComponent
    {
        private readonly IVector3Component vector3Component;
        private readonly ISceneComponent originalSceneComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvertedSceneComponent"/> class.
        /// </summary>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        /// <param name="originalSceneComponent">The original scene component.</param>
        public InvertedSceneComponent(
            IVector3Component vector3Component,
            ISceneComponent originalSceneComponent)
        {
            this.vector3Component = vector3Component;
            this.originalSceneComponent = originalSceneComponent;
        }

        /// <inheritdoc/>
        public bool Contains(Vector3 point)
        {
            return !this.originalSceneComponent.Contains(point);
        }

        /// <inheritdoc/>
        public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
        {
            return this.originalSceneComponent.GetAllIntercepts(lineOfSight)
                .Select(intercept => new Intercept
                {
                    Color = intercept.Color,
                    Distance = intercept.Distance,
                    Normal = () => this.vector3Component.Multiply(intercept.Normal(), -1D),
                })
                .ToList();
        }
    }
}
