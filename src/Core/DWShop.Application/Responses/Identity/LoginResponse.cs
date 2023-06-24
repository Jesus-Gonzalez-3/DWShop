using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Application.Responses.Identity
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Gafette { get; set; }
    }
}
