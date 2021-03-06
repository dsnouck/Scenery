// <copyright file="IntersectionValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators.SceneValidators;

using FluentValidation;
using Scenery.Components.Interfaces;
using Scenery.Models.Scenes;

/// <summary>
/// Represents a validator for <see cref="Intersection"/>.
/// </summary>
public class IntersectionValidator : AbstractValidator<Intersection>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntersectionValidator"/> class.
    /// </summary>
    /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
    public IntersectionValidator(IVector3Component vector3Component)
    {
        this.RuleFor(scene => scene.Scenes)
            .NotNull();
        this.RuleForEach(scene => scene.Scenes)
            .NotNull()
            .SetValidator(new SceneValidator(vector3Component));
    }
}
