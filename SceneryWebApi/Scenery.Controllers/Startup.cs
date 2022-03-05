// <copyright file="Startup.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers;

using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Scenery.Components;
using Scenery.Controllers.Converters;
using Scenery.Controllers.Validators;

/// <summary>
/// The startup class.
/// </summary>
public class Startup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">An <see cref="IConfiguration"/>.</param>
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">An <see cref="IServiceCollection"/>.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
        });
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new SceneJsonConverter()));
        services.AddComponents()
            .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<SceneContainerValidator>());
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Scenery web API",
                Description = "An ASP.NET Core web API for rendering scene containers.",
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);
            options.UseAllOfForInheritance();
            options.SchemaFilter<ExampleSchemaFilter>();
        });
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="applicationBuilder">An <see cref="IApplicationBuilder"/>.</param>
    /// <param name="environment">An <see cref="IWebHostEnvironment"/>.</param>
    public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment environment)
    {
        applicationBuilder.UseCors();
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI(options => options.EnableTryItOutByDefault());

        if (environment.IsDevelopment())
        {
            applicationBuilder.UseDeveloperExceptionPage();
        }

        applicationBuilder.UseHttpsRedirection();
        applicationBuilder.UseRouting();
        applicationBuilder.UseAuthorization();
        applicationBuilder.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
