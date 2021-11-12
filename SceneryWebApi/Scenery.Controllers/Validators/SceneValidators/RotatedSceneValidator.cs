// <copyright file="RotatedSceneValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators.SceneValidators
{
    using FluentValidation;
    using Scenery.Components.Interfaces;
    using Scenery.Models.Scenes;

    /// <summary>
    /// Represents a validator for <see cref="RotatedScene"/>.
    /// </summary>
    public class RotatedSceneValidator : AbstractValidator<RotatedScene>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RotatedSceneValidator"/> class.
        /// </summary>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        public RotatedSceneValidator(IVector3Component vector3Component)
        {
            this.RuleFor(scene => scene.Axis)
                .NotNull()
                .Must(axis => vector3Component.GetLength(axis) > 0D)
                .WithMessage("Axis must not have zero length.");
            this.RuleFor(scene => scene.OriginalScene)
                .NotNull()
                .SetValidator(new SceneValidator(vector3Component));
        }
    }
}
