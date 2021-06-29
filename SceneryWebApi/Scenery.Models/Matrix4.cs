// <copyright file="Matrix4.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Models
{
    /// <summary>
    /// Represents a four by four matrix.
    /// </summary>
    public class Matrix4
    {
        /// <summary>
        /// Gets or sets the first row.
        /// </summary>
        public Vector4 FirstRow { get; set; } = new Vector4();

        /// <summary>
        /// Gets or sets the second row.
        /// </summary>
        public Vector4 SecondRow { get; set; } = new Vector4();

        /// <summary>
        /// Gets or sets the third row.
        /// </summary>
        public Vector4 ThirdRow { get; set; } = new Vector4();

        /// <summary>
        /// Gets or sets the fourth row.
        /// </summary>
        public Vector4 FourthRow { get; set; } = new Vector4();
    }
}
