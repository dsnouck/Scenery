// <copyright file="ISamplerComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;

/// <summary>
/// Provides sampling operations.
/// </summary>
public interface ISamplerComponent
{
    /// <summary>
    /// Samples an image to a bitmap.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="samplerSettings">The sampler settings.</param>
    /// <returns>The image sampled to a bitmap.</returns>
    List<List<Color>> SampleImageToBitmap(Func<Vector2, Color> image, SamplerSettings samplerSettings);
}
