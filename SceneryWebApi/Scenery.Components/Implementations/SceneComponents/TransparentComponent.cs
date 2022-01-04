// <copyright file="TransparentComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents;

using System.Collections.Generic;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;

/// <inheritdoc/>
public class TransparentComponent : ISceneComponent
{
    private readonly ISceneComponent sceneComponent;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransparentComponent"/> class.
    /// </summary>
    /// <param name="sceneComponent">The original scene component.</param>
    public TransparentComponent(
        ISceneComponent sceneComponent)
    {
        this.sceneComponent = sceneComponent;
    }

    /// <inheritdoc/>
    public bool Contains(Vector3 point)
    {
        return this.sceneComponent.Contains(point);
    }

    /// <inheritdoc/>
    public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
    {
        return new List<Intercept>();
    }
}
