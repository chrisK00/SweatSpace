using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
