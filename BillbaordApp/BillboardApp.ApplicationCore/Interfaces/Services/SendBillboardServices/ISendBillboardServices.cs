using BillboardApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillboardApp.ApplicationCore.Interfaces.Services.SendBillboardServices
{
    public interface ISendBillboardServices
    {
        Task<Billboard> SendBillboard(int idDevice, Billboard billboard);
    }
}
