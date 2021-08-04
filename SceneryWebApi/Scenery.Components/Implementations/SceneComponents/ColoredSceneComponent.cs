// <copyright file="ColoredSceneComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents
{
    using System.Collections.Generic;
    using System.Linq;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;

    /// <inheritdoc/>
    public class ColoredSceneComponent : ISceneComponent
    {
        private readonly ISceneComponent originalSceneComponent;
        private readonly Color color;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColoredSceneComponent"/> class.
        /// </summary>
        /// <param name="originalSceneComponent">The original scene component.</param>
        /// <param name="color">The color.</param>
        public ColoredSceneComponent(
            ISceneComponent originalSceneComponent,
            Color color)
        {
            this.originalSceneComponent = originalSceneComponent;
            this.color = color;
        }

        /// <inheritdoc/>
        public bool Contains(Vector3 point)
        {
            return this.originalSceneComponent.Contains(point);
        }

        /// <inheritdoc/>
        public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
        {
            return this.originalSceneComponent.GetAllIntercepts(lineOfSight)
                .Select(intercept => new Intercept
                {
                    Color = this.color,
                    Distance = intercept.Distance,
                    Normal = intercept.Normal,
                })
                .ToList();
        }
    }
}
