using BillboardApp.ApplicationCore.Interfaces.Services.ConfigurationBillboardServices;
using BillboardApp.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using System.Data.SqlClient;
using Dapper;

namespace BillboardApp.Infrastructure.Services.ConfigurationBillboardServices
{
    public class ConfigurationBillboardServices : IConfigurationBillboardServices
    {
        private readonly string _connectionString;
        public string iothubowner;
        static ServiceClient serviceClient;
        private readonly ILogger<ConfigurationBillboardServices> _logger;

        public ConfigurationBillboardServices(IConfiguration configuration, ILogger<ConfigurationBillboardServices> logger)
        {
            this._logger = logger;
            this._connectionString = configuration.GetConnectionString("db");
            this.iothubowner = configuration.GetConnectionString("iothubowner");
            serviceClient = ServiceClient.CreateFromConnectionString(this.iothubowner);
        }
        public async Task<configurationBillboard> SetConfigurationBillboard(int idDevice, configurationBillboard configuration)  
        {
            //Take the name of Device to database
            const string query = @"SELECT [deviceId] FROM [dbo].[Chiki_Test] WHERE Id = @idDevice";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string id = await connection.QueryFirstOrDefaultAsync<string>(query, new { idDevice });

            //Set the configuration billboard in Device Tiwin of Device
            var registryManager = RegistryManager.CreateFromConnectionString(this.iothubowner);
            var twin = await registryManager.GetTwinAsync(id);
            twin.Properties.Desired["rows"] = configuration.rows;
            twin.Properties.Desired["color"] = configuration.color;
            twin.Properties.Desired["columns"] = configuration.columns;
            await registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);

            return configuration;
        }
    }
}
