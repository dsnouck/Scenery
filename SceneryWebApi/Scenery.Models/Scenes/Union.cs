﻿// <copyright file="Union.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes;

/// <inheritdoc/>
public class Union : Scene
{
    /// <summary>
    /// Gets or sets the scenes to be united.
    /// </summary>
    public List<Scene> Scenes { get; set; } = new List<Scene>();
}
