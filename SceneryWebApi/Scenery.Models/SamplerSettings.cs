// <copyright file="SamplerSettings.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models;

/// <summary>
/// Contains sampler settings.
/// </summary>
public class SamplerSettings
{
    /// <summary>
    /// Gets or sets the number of columns.
    /// </summary>
    public int Columns { get; set; } = 128;

    /// <summary>
    /// Gets or sets the number of rows.
    /// </summary>
    public int Rows { get; set; } = 128;

    /// <summary>
    /// Gets or sets the number of subsamples.
    /// </summary>
    public int Subsamples { get; set; } = 2;
}
