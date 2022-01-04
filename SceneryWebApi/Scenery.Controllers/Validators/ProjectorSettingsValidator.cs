// <copyright file="ProjectorSettingsValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators;

using FluentValidation;
using Scenery.Components.Interfaces;
using Scenery.Models;

/// <summary>
/// Represents a validator for <see cref="ProjectorSettings"/>.
/// </summary>
public class ProjectorSettingsValidator : AbstractValidator<ProjectorSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectorSettingsValidator"/> class.
    /// </summary>
    /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
    public ProjectorSettingsValidator(IVector3Component vector3Component)
    {
        this.RuleFor(projectorSettings => projectorSettings.Eye)
            .NotNull();
        this.RuleFor(projectorSettings => projectorSettings.Focus)
            .NotNull();
        this.RuleFor(projectorSettings => projectorSettings)
            .Must(projectorSettings => vector3Component.GetLength(vector3Component.Subtract(projectorSettings.Focus, projectorSettings.Eye)) > 0D)
            .WithMessage("Eye and focus must not coincide.");
        this.RuleFor(projectorSettings => projectorSettings.Background)
            .NotNull()
            .SetValidator(new ColorValidator());
    }
}
