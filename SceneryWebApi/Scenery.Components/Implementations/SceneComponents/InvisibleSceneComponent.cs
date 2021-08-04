// <copyright file="InvisibleSceneComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents
{
    using System.Collections.Generic;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;

    /// <inheritdoc/>
    public class InvisibleSceneComponent : ISceneComponent
    {
        private readonly ISceneComponent originalSceneComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvisibleSceneComponent"/> class.
        /// </summary>
        /// <param name="originalSceneComponent">The original scene component.</param>
        public InvisibleSceneComponent(
            ISceneComponent originalSceneComponent)
        {
            this.originalSceneComponent = originalSceneComponent;
        }

        /// <inheritdoc/>
        public bool Contains(Vector3 point)
        {
            return this.originalSceneComponent.Contains(point);
        }

        /// <inheritdoc/>
        public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
        {
            return new List<Intercept>();
        }
    }
}
