using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridgeSolu.BLL
{
    public class AuthenticateUser
    {
        public bool CheckAdmin(int userId)
        {
            return userId < 10;
        }
    }
}
