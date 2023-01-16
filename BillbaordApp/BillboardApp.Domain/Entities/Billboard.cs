using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillboardApp.Domain.Entities
{
    public class Billboard
    {
        public List<string> rows { get; set; }

        public Billboard()
        {
            this.rows = new List<string>();
        }
    }
}
