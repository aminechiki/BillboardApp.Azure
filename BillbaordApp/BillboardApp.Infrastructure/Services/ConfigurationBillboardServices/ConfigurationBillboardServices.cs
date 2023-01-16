using BillboardApp.ApplicationCore.Interfaces.Services.ConfigurationBillboardServices;
using BillboardApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillboardApp.Infrastructure.Services.ConfigurationBillboardServices
{
    public class ConfigurationBillboardServices : IConfigurationBillboardServices
    {
        public Task<configurationBillboard> SetConfigurationBillboard(int idDevice, configurationBillboard configuration)
        {
            throw new NotImplementedException();
        }
    }
}
