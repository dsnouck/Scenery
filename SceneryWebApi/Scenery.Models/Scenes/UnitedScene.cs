// <copyright file="UnitedScene.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public class UnitedScene : Scene
    {
        /// <summary>
        /// Gets or sets the scenes to be united.
        /// </summary>
        public List<Scene> Scenes { get; set; } = new List<Scene>();
    }
}
