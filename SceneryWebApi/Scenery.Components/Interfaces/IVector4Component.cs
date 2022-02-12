﻿// <copyright file="IVector4Component.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;

/// <summary>
/// Provides operations concerning <see cref="Vector4"/>s.
/// </summary>
public interface IVector4Component
{
    /// <summary>
    /// Calculates the dot product between two vectors.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="otherVector">The other vector.</param>
    /// <returns>The dot product between the vectors.</returns>
    double DotProduct(Vector4 vector, Vector4 otherVector);
}
