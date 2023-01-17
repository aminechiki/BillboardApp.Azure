using BillboardApp.ApplicationCore.Interfaces.Services.SendBillboardServices;
using BillboardApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BillboardApp.Web.Controller.SendBillboardController
{
    [ApiController]
    [Route("api/message")]
    public class MessaggeController : ControllerBase
    {
        private readonly ILogger<MessaggeController> _logger;
        private readonly ISendBillboardServices _sendBillboardServices1;
        public MessaggeController(ILogger<MessaggeController> logger, ISendBillboardServices sendBillboardServices)
        {
            _logger = logger;
            _sendBillboardServices1 = sendBillboardServices;
        }

        [HttpPost("{idDevice}")]
        public Task SendText(int idDevice, Billboard billboard)
        {
            return this._sendBillboardServices1.SendBillboard(idDevice, billboard);
        }
    }
}