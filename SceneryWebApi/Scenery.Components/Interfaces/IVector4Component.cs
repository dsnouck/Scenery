// <copyright file="IVector4Component.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces
{
    using Scenery.Models;

    /// <summary>
    /// Provides operations concerning <see cref="Vector4"/>s.
    /// </summary>
    public interface IVector4Component
    {
        /// <summary>
        /// Calculates the dot product between <paramref name="vector"/> and <paramref name="otherVector"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="otherVector">The other vector.</param>
        /// <returns>The dot product between <paramref name="vector"/> and <paramref name="otherVector"/>.</returns>
        double DotProduct(Vector4 vector, Vector4 otherVector);
    }
}
