// <copyright file="SurfaceIntersection.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models;

/// <summary>
/// Represents an intersection with the surface of a scene.
/// </summary>
public class SurfaceIntersection
{
    /// <summary>
    /// Gets or sets the color.
    /// </summary>
    public Color Color { get; set; } = new Color
    {
        R = 0D,
        G = 0D,
        B = 1D,
    };

    /// <summary>
    /// Gets or sets the distance.
    /// </summary>
    public double Distance { get; set; } = 0D;

    /// <summary>
    /// Gets or sets the normal.
    /// </summary>
    /// <remarks>
    /// Is lazy because we only need to calculate this for the first surface intersection along a line of sight.
    /// </remarks>
    public Func<Vector3> Normal { get; set; } = () => new Vector3();
}
