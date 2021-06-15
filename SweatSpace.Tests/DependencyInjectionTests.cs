using System;
using Microsoft.Extensions.Hosting;
using SweatSpace.Api;
using Xunit;

namespace SweatSpace.Tests
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void VerifyDISetup()
        {
            Program.CreateHostBuilder(Array.Empty<string>())
                .UseDefaultServiceProvider(config => config.ValidateOnBuild = true).Build();
        }
    }
}
