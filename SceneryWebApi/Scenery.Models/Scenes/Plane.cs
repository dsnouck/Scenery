// <copyright file="Plane.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes;

/// <summary>
/// A plane.
/// </summary>
/// <remarks>
/// The plane is suspended one normal vector away from the origin and perpendicular to the normal vector.
/// Because a scene must have an interior and an exterior, this is actually a halfspace.
/// The plane splits the space into two halfspaces.
/// This is the one containing the origin.
/// </remarks>
public class Plane : Scene
{
    /// <summary>
    /// Gets or sets the normal.
    /// </summary>
    public Vector3 Normal { get; set; } = new Vector3
    {
        X = 0D,
        Y = 0D,
        Z = -1D,
    };
}
