// <copyright file="IBitmapFileComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;

/// <summary>
/// Provides operations concerning bitmap files.
/// </summary>
public interface IBitmapFileComponent
{
    /// <summary>
    /// Creates a bitmap file from a bitmap.
    /// </summary>
    /// <param name="bitmap">The bitmap.</param>
    /// <returns>The bitmap file created from the bitmap.</returns>
    Stream CreateBitmapFile(List<List<Color>> bitmap);
}
