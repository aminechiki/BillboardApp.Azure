using BillboardApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillboardApp.ApplicationCore.Interfaces.Services.ConfigurationBillboardServices
{
    public interface IConfigurationBillboardServices
    {
        Task<configurationBillboard> SetConfigurationBillboard(int idDevice, configurationBillboard configuration);
    }
}
