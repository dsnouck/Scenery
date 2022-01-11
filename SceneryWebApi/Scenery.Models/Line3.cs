// <copyright file="Line3.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models;

/// <summary>
/// Represents a line in three dimensions.
/// </summary>
public class Line3
{
    /// <summary>
    /// Gets or sets the origin.
    /// </summary>
    public Vector3 Origin { get; set; } = new Vector3();

    /// <summary>
    /// Gets or sets the direction.
    /// </summary>
    public Vector3 Direction { get; set; } = new Vector3
    {
        X = 1D,
        Y = 0D,
        Z = 0D,
    };
}
