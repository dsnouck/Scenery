// <copyright file="SceneValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators.SceneValidators
{
    using FluentValidation;
    using FluentValidation.Results;
    using Scenery.Components.Interfaces;
    using Scenery.Models.Scenes;

    /// <summary>
    /// Represents a validator for <see cref="Scene"/>.
    /// </summary>
    public class SceneValidator : AbstractValidator<Scene>
    {
        private readonly IVector3Component vector3Component;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneValidator"/> class.
        /// </summary>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        public SceneValidator(IVector3Component vector3Component)
        {
            this.vector3Component = vector3Component;
            this.RuleFor(scene => scene)
                .Must(scene => scene.GetType() != typeof(Scene))
                .WithMessage("Scene must not be of base type Scene.");
        }

        /// <inheritdoc/>
        public override ValidationResult Validate(ValidationContext<Scene> context)
        {
            return context.InstanceToValidate switch
            {
                Colored colored => new ColoredValidator(this.vector3Component).Validate(context.CloneForChildValidator(colored)),
                Intersection intersection => new IntersectionValidator(this.vector3Component).Validate(context.CloneForChildValidator(intersection)),
                Inverted inverted => new InvertedValidator(this.vector3Component).Validate(context.CloneForChildValidator(inverted)),
                Transparent transparent => new TransparentValidator(this.vector3Component).Validate(context.CloneForChildValidator(transparent)),
                Plane plane => new PlaneValidator().Validate(context.CloneForChildValidator(plane)),
                Rotated rotated => new RotatedValidator(this.vector3Component).Validate(context.CloneForChildValidator(rotated)),
                Scaled scaled => new ScaledValidator(this.vector3Component).Validate(context.CloneForChildValidator(scaled)),
                Translated translated => new TranslatedValidator(this.vector3Component).Validate(context.CloneForChildValidator(translated)),
                Union union => new UnionValidator(this.vector3Component).Validate(context.CloneForChildValidator(union)),
                _ => base.Validate(context)
            };
        }
    }
}
