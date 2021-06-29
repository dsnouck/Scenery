// <copyright file="PlaneScene.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    /// <inheritdoc/>
    public class PlaneScene : Scene
    {
        /// <summary>
        /// Gets or sets the normal.
        /// </summary>
        public Vector3 Normal { get; set; } = new Vector3
        {
            XCoordinate = 0D,
            YCoordinate = 0D,
            ZCoordinate = -1D,
        };
    }
}
