// <copyright file="IFuncVector2Vector3Component.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces
{
    using Scenery.Models;
    using System;

    /// <summary>
    /// Provides operations concerning <see cref="Func{Vector2,Vector3}"/>s.
    /// </summary>
    public interface IFuncVector2Vector3Component
    {
        /// <summary>
        /// Gets the parametric plane containing <paramref name="origin"/> along <paramref name="xDirection"/> and <paramref name="yDirection"/>.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="xDirection">The x-direction.</param>
        /// <param name="yDirection">The y-direction.</param>
        /// <returns>The parametric plane containing <paramref name="origin"/> along <paramref name="xDirection"/> and <paramref name="yDirection"/>.</returns>
        Func<Vector2, Vector3> GetPlane(Vector3 origin, Vector3 xDirection, Vector3 yDirection);
    }
}
