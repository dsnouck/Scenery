// <copyright file="Program.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers;

/// <summary>
/// Contains the main entry point.
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    public static void Main(string[] arguments)
    {
        CreateHostBuilder(arguments).Build().Run();
    }

    /// <summary>
    /// Creates the host builder.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    /// <returns>The host builder.</returns>
    public static IHostBuilder CreateHostBuilder(string[] arguments)
    {
        return Host.CreateDefaultBuilder(arguments)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
