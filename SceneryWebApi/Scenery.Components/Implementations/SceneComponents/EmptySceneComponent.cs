﻿// <copyright file="EmptySceneComponent.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents
{
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;
    using System.Collections.Generic;

    /// <inheritdoc/>
    public class EmptySceneComponent : ISceneComponent
    {
        /// <inheritdoc/>
        public bool Contains(Vector3 point)
        {
            return false;
        }

        /// <inheritdoc/>
        public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
        {
            return new List<Intercept>();
        }
    }
}
