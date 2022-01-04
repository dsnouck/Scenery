// <copyright file="Vector4Component.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations;

using System;
using Scenery.Components.Interfaces;
using Scenery.Models;

/// <inheritdoc/>
public class Vector4Component : IVector4Component
{
    /// <inheritdoc/>
    public double DotProduct(Vector4 vector, Vector4 otherVector)
    {
        ArgumentNullException.ThrowIfNull(vector);
        ArgumentNullException.ThrowIfNull(otherVector);

        return
            (vector.X * otherVector.X) +
            (vector.Y * otherVector.Y) +
            (vector.Z * otherVector.Z) +
            (vector.W * otherVector.W);
    }
}
