// <copyright file="RotatedValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators.SceneValidators;

using FluentValidation;
using Scenery.Components.Interfaces;
using Scenery.Models.Scenes;

/// <summary>
/// Represents a validator for <see cref="Rotated"/>.
/// </summary>
public class RotatedValidator : AbstractValidator<Rotated>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RotatedValidator"/> class.
    /// </summary>
    /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
    public RotatedValidator(IVector3Component vector3Component)
    {
        this.RuleFor(scene => scene.Axis)
            .NotNull()
            .Must(axis => vector3Component.Length(axis) > 0D)
            .WithMessage("Axis must not have zero length.");
        this.RuleFor(scene => scene.Scene)
            .NotNull()
            .SetValidator(new SceneValidator(vector3Component));
    }
}
