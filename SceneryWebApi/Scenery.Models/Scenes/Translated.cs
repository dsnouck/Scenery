// <copyright file="Translated.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes;

/// <summary>
/// A translated scene.
/// </summary>
public class Translated : Transformed
{
    /// <summary>
    /// Gets or sets the translation.
    /// </summary>
    public Vector3 Translation { get; set; } = new Vector3
    {
        X = 0D,
        Y = 0D,
        Z = 1D,
    };
}
