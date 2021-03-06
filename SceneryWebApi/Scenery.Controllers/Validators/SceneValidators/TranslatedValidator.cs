// <copyright file="TranslatedValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators.SceneValidators;

using FluentValidation;
using Scenery.Components.Interfaces;
using Scenery.Models.Scenes;

/// <summary>
/// Represents a validator for <see cref="Translated"/>.
/// </summary>
public class TranslatedValidator : AbstractValidator<Translated>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TranslatedValidator"/> class.
    /// </summary>
    /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
    public TranslatedValidator(IVector3Component vector3Component)
    {
        this.RuleFor(scene => scene.Translation)
            .NotNull();
        this.RuleFor(scene => scene.Scene)
            .NotNull()
            .SetValidator(new SceneValidator(vector3Component));
    }
}
