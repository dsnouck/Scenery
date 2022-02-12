// <copyright file="ProjectorSettings.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models;

/// <summary>
/// Contains settings used when projecting a scene.
/// </summary>
public class ProjectorSettings
{
    /// <summary>
    /// Gets or sets the eye, i.e. the point we are looking from.
    /// </summary>
    public Vector3 Eye { get; set; } = new Vector3
    {
        X = 2D,
        Y = 2D,
        Z = 2D,
    };

    /// <summary>
    /// Gets or sets the focus, i.e. the point we are looking at.
    /// </summary>
    public Vector3 Focus { get; set; } = new Vector3();

    /// <summary>
    /// Gets or sets the horizontal field of view.
    /// </summary>
    public double FieldOfView { get; set; } = Math.PI / 4D;

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    public Color Background { get; set; } = new Color();
}
