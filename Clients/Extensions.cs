using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Clients
{
    public static class Extensions
    {
        public static void AddHttpClientOption(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
        }
    }
}
