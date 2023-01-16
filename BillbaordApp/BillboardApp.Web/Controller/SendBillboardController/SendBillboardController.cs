using BillboardApp.ApplicationCore.Interfaces.Services.ConfigurationBillboardServices;
using BillboardApp.Domain.Entities;
using BillboardApp.Infrastructure.Services.ConfigurationBillboardServices;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BillboardApp.Web.Controller.SendBillboardController
{

        [ApiController]
        [Route("api/[controller]")]
        public class SendBillboardController : ControllerBase
        {
            private readonly ILogger<SendBillboardController> _logger;
            private readonly IConfigurationBillboardServices _configurationBillboardServices;
            public SendBillboardController(ILogger<SendBillboardController> logger, IConfigurationBillboardServices configurationBillboardServices)
            {
            this._logger = logger;
            this._configurationBillboardServices = configurationBillboardServices;
            }

            [HttpPost("{idDevice}")]

            public Task SetConfigurationBillboard([FromRoute] int idDevice, configurationBillboard configuration)
            {
                return _configurationBillboardServices.SetConfigurationBillboard(idDevice, configuration);
            }
        }
    
}
