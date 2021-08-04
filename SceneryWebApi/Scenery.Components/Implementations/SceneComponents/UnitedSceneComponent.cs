// <copyright file="UnitedSceneComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents
{
    using System;
    using System.Collections.Generic;
    using Scenery.Components.Interfaces;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;

    /// <inheritdoc/>
    public class UnitedSceneComponent : ISceneComponent
    {
        private readonly ILine3Component line3Component;
        private readonly ISceneComponent originalSceneComponent;
        private readonly ISceneComponent otherOriginalSceneComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitedSceneComponent"/> class.
        /// </summary>
        /// <param name="line3Component">An <see cref="ILine3Component"/>.</param>
        /// <param name="originalSceneComponent">The original <see cref="ISceneComponent"/>.</param>
        /// <param name="otherOriginalSceneComponent">The other original <see cref="ISceneComponent"/>.</param>
        public UnitedSceneComponent(
            ILine3Component line3Component,
            ISceneComponent originalSceneComponent,
            ISceneComponent otherOriginalSceneComponent)
        {
            this.line3Component = line3Component;
            this.originalSceneComponent = originalSceneComponent;
            this.otherOriginalSceneComponent = otherOriginalSceneComponent;
        }

        /// <inheritdoc/>
        public bool Contains(Vector3 point)
        {
            return this.originalSceneComponent.Contains(point) ||
                this.otherOriginalSceneComponent.Contains(point);
        }

        /// <inheritdoc/>
        public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
        {
            if (lineOfSight == null)
            {
                throw new ArgumentNullException(nameof(lineOfSight));
            }

            var allIntercepts = new List<Intercept>();

            var originalSceneIntercepts = this.originalSceneComponent.GetAllIntercepts(lineOfSight);
            foreach (var intercept in originalSceneIntercepts)
            {
                var point = this.line3Component.GetPointAtDistance(lineOfSight, intercept.Distance);
                if (!this.otherOriginalSceneComponent.Contains(point))
                {
                    allIntercepts.Add(intercept);
                }
            }

            var otherOriginalSceneIntercepts = this.otherOriginalSceneComponent.GetAllIntercepts(lineOfSight);
            foreach (var intercept in otherOriginalSceneIntercepts)
            {
                var point = this.line3Component.GetPointAtDistance(lineOfSight, intercept.Distance);
                if (!this.originalSceneComponent.Contains(point))
                {
                    allIntercepts.Add(intercept);
                }
            }

            return allIntercepts;
        }
    }
}
