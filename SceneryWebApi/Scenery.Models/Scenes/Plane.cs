// <copyright file="Plane.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    /// <inheritdoc/>
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
}
