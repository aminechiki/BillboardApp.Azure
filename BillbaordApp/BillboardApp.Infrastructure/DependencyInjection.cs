using BillboardApp.ApplicationCore.Interfaces.Services.ConfigurationBillboardServices;
using BillboardApp.ApplicationCore.Interfaces.Services.SendBillboardServices;
using BillboardApp.Infrastructure.Services.ConfigurationBillboardServices;
using BillboardApp.Infrastructure.Services.SendBillboardServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillboardApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationBillboardServices, ConfigurationBillboardServices>();
            services.AddSingleton<ISendBillboardServices, SendBillboardServices>();

            return services;
        }
    }
}
