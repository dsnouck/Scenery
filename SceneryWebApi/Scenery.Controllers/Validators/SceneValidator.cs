// <copyright file="SceneValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators
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
            // TODO: Create issue for updates.
            // TODO: Create issue for .editorConfig.
            // TODO: Create issue or use existing for folder structure. Scenes under Validators? SceneFactory in Scenes?
            return context.InstanceToValidate switch
            {
                ColoredScene coloredScene => new ColoredSceneValidator(this.vector3Component).Validate(context.CloneForChildValidator(coloredScene)),
                IntersectedScene intersectedScene => new IntersectedSceneValidator(this.vector3Component).Validate(context.CloneForChildValidator(intersectedScene)),
                InvertedScene invertedScene => new InvertedSceneValidator(this.vector3Component).Validate(context.CloneForChildValidator(invertedScene)),
                InvisibleScene invisibleScene => new InvisibleSceneValidator(this.vector3Component).Validate(context.CloneForChildValidator(invisibleScene)),
                PlaneScene planeScene => new PlaneSceneValidator().Validate(context.CloneForChildValidator(planeScene)),
                RotatedScene rotatedScene => new RotatedSceneValidator(this.vector3Component).Validate(context.CloneForChildValidator(rotatedScene)),
                ScaledScene scaledScene => new ScaledSceneValidator(this.vector3Component).Validate(context.CloneForChildValidator(scaledScene)),
                TranslatedScene translatedScene => new TranslatedSceneValidator(this.vector3Component).Validate(context.CloneForChildValidator(translatedScene)),
                UnitedScene unitedScene => new UnitedSceneValidator(this.vector3Component).Validate(context.CloneForChildValidator(unitedScene)),
                _ => base.Validate(context)
            };
        }
    }
}
