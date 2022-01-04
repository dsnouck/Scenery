// <copyright file="InvertedComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents;

using System.Collections.Generic;
using System.Linq;
using Scenery.Components.Interfaces;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;

/// <inheritdoc/>
public class InvertedComponent : ISceneComponent
{
    private readonly IVector3Component vector3Component;
    private readonly ISceneComponent sceneComponent;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvertedComponent"/> class.
    /// </summary>
    /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
    /// <param name="sceneComponent">The original scene component.</param>
    public InvertedComponent(
        IVector3Component vector3Component,
        ISceneComponent sceneComponent)
    {
        this.vector3Component = vector3Component;
        this.sceneComponent = sceneComponent;
    }

    /// <inheritdoc/>
    public bool Contains(Vector3 point)
    {
        return !this.sceneComponent.Contains(point);
    }

    /// <inheritdoc/>
    public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
    {
        return this.sceneComponent.GetAllIntercepts(lineOfSight)
            .Select(intercept => new Intercept
            {
                Color = intercept.Color,
                Distance = intercept.Distance,
                Normal = () => this.vector3Component.Multiply(intercept.Normal(), -1D),
            })
            .ToList();
    }
}
