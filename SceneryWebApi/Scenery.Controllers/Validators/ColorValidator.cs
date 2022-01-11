// <copyright file="ColorValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators;

using FluentValidation;
using Scenery.Models;

/// <summary>
/// Represents a validator for <see cref="Color"/>.
/// </summary>
public class ColorValidator : AbstractValidator<Color>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorValidator"/> class.
    /// </summary>
    public ColorValidator()
    {
        this.RuleFor(color => color.R)
            .GreaterThanOrEqualTo(0D)
            .LessThanOrEqualTo(1D);
        this.RuleFor(color => color.G)
            .GreaterThanOrEqualTo(0D)
            .LessThanOrEqualTo(1D);
        this.RuleFor(color => color.B)
            .GreaterThanOrEqualTo(0D)
            .LessThanOrEqualTo(1D);
    }
}
