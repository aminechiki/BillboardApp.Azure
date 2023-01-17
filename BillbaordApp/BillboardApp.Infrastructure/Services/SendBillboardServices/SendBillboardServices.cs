using BillboardApp.ApplicationCore.Interfaces.Services.SendBillboardServices;
using BillboardApp.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Data.SqlClient;
using Dapper;

namespace BillboardApp.Infrastructure.Services.SendBillboardServices
{
    public class SendBillboardServices : ISendBillboardServices
    {
        static ServiceClient serviceClient;
        private readonly string _connectionString;
        public string iothubowner;
        private readonly ILogger<ISendBillboardServices> _logger;

        public SendBillboardServices(IConfiguration configuration, ILogger<SendBillboardServices> logger)
        {
            this._logger = logger;
            this._connectionString = configuration.GetConnectionString("db");
            this.iothubowner = configuration.GetConnectionString("iothubowner");
            serviceClient = ServiceClient.CreateFromConnectionString(this.iothubowner);
        }
        public async Task<Billboard> SendBillboard(int idDevice, Billboard billboard)
        {
            string billboardSerialize = JsonSerializer.Serialize(billboard);
            SendCloudToDeviceMessageAsync(idDevice, billboardSerialize).Wait();
            return billboard;
        }

        private async Task SendCloudToDeviceMessageAsync(int idDevice, string billboardSerialize)              
        {
            //recover id device from database
            const string query = @"SELECT [deviceId] FROM [dbo].[Chiki_Test] WHERE Id = @idDevice";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string id = await connection.QueryFirstOrDefaultAsync<string>(query, new { idDevice });

            //create a object Message with inside billboardSerialize object serialize
            using var message = new Message(Encoding.ASCII.GetBytes(billboardSerialize))
            {
                ContentType = "application/json",
                ContentEncoding = "utf-8",
            };

            //send a message to device 
            await serviceClient.SendAsync(id, message);
        }

    }
}
