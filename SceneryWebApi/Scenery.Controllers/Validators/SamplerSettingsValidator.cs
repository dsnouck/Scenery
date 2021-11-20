// <copyright file="SamplerSettingsValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators
{
    using FluentValidation;
    using Scenery.Models;

    /// <summary>
    /// Represents a validator for <see cref="SamplerSettings"/>.
    /// </summary>
    public class SamplerSettingsValidator : AbstractValidator<SamplerSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SamplerSettingsValidator"/> class.
        /// </summary>
        public SamplerSettingsValidator()
        {
            this.RuleFor(samplerSettings => samplerSettings.Columns)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(7680);
            this.RuleFor(samplerSettings => samplerSettings.Rows)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(4320);
            this.RuleFor(samplerSettings => samplerSettings.Subsamples)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(16);
        }
    }
}
