using BillboardApp.ApplicationCore.Interfaces.Services.SendBillboardServices;
using BillboardApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillboardApp.Infrastructure.Services.SendBillboardServices
{
    public class SendBillboardServices : ISendBillboardServices
    {
        public Task<Billboard> SendBillboard(int idDevice, Billboard billboard)
        {
            throw new NotImplementedException();
        }
    }
}
