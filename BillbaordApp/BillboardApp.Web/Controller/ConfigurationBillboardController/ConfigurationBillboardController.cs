using BillboardApp.ApplicationCore.Interfaces.Services.ConfigurationBillboardServices;
using BillboardApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BillboardApp.Web.Controller.ConfigurationBillboardController
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class ConfigurationBillboardController : ControllerBase
        {
            private readonly ILogger<ConfigurationBillboardController> _logger;
            private readonly IConfigurationBillboardServices _configurationBillboardServices;
            public ConfigurationBillboardController(ILogger<ConfigurationBillboardController> logger, IConfigurationBillboardServices configurationBillboardServices)
            {
                this._logger = logger;
                this._configurationBillboardServices = configurationBillboardServices;
            }

            [HttpPost("{idDevice}")]
            public Task SendText(int id, configurationBillboard billboard)
            {
                return _configurationBillboardServices.SetConfigurationBillboard(id, billboard);
            }


        
    }
}
