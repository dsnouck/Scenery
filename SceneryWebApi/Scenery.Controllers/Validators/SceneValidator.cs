// <copyright file="SceneValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators
{
    using FluentValidation;
    using FluentValidation.Results;
    using Scenery.Models.Scenes;

    /// <summary>
    /// Represents a validator for <see cref="Scene"/>s.
    /// </summary>
    public class SceneValidator : AbstractValidator<Scene>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneValidator"/> class.
        /// </summary>
        public SceneValidator()
        {
            this.RuleFor(scene => scene)
                .Must(scene => scene.GetType() != typeof(Scene))
                .WithMessage("Scene must not be of base type Scene.");
        }

        /// <inheritdoc/>
        public override ValidationResult Validate(ValidationContext<Scene> context)
        {
            // TODO: Add validators for those scenes that need one.
            return context.InstanceToValidate switch
            {
                ScaledScene scaledScene => new ScaledSceneValidator().Validate(context.CloneForChildValidator(scaledScene)),
                _ => base.Validate(context)
            };
        }
    }
}
