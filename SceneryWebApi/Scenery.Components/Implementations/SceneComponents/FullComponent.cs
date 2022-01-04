// <copyright file="FullComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents;

using System.Collections.Generic;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;

/// <inheritdoc/>
public class FullComponent : ISceneComponent
{
    /// <inheritdoc/>
    public bool Contains(Vector3 point)
    {
        return true;
    }

    /// <inheritdoc/>
    public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
    {
        return new List<Intercept>();
    }
}
