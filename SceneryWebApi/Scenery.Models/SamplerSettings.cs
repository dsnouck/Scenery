// <copyright file="SamplerSettings.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models
{
    /// <summary>
    /// Contains sampler settings.
    /// </summary>
    public class SamplerSettings
    {
        /// <summary>
        /// Gets or sets the column count.
        /// </summary>
        public int ColumnCount { get; set; } = 128;

        /// <summary>
        /// Gets or sets the row count.
        /// </summary>
        public int RowCount { get; set; } = 128;

        /// <summary>
        /// Gets or sets the subsample count.
        /// </summary>
        public int SubsampleCount { get; set; } = 2;
    }
}
