// <copyright file="ExampleSchemaFilter.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Scenery.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
/// An <see cref="ISchemaFilter"/> that provides a valid example of a <see cref="SceneContainer"/>.
/// </summary>
public class ExampleSchemaFilter : ISchemaFilter
{
    /// <inheritdoc/>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(SceneContainer))
        {
            schema.Example = new OpenApiObject
            {
                ["scene"] = new OpenApiObject
                {
                    ["type"] = new OpenApiString("sphere"),
                },
                ["projector"] = new OpenApiObject
                {
                    ["eye"] = new OpenApiObject
                    {
                        ["x"] = new OpenApiDouble(2D),
                        ["y"] = new OpenApiDouble(2D),
                        ["z"] = new OpenApiDouble(2D),
                    },
                    ["focus"] = new OpenApiObject
                    {
                        ["x"] = new OpenApiDouble(0D),
                        ["y"] = new OpenApiDouble(0D),
                        ["z"] = new OpenApiDouble(0D),
                    },
                    ["fieldOfView"] = new OpenApiDouble(0.78539816339744828D),
                    ["background"] = new OpenApiObject
                    {
                        ["r"] = new OpenApiDouble(0D),
                        ["g"] = new OpenApiDouble(0D),
                        ["b"] = new OpenApiDouble(0D),
                    },
                },
                ["sampler"] = new OpenApiObject
                {
                    ["columns"] = new OpenApiInteger(128),
                    ["rows"] = new OpenApiInteger(128),
                    ["subsamples"] = new OpenApiInteger(2),
                },
            };
        }
    }
}
