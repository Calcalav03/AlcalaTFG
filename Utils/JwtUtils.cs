using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.Utils
{
    class JwtUtils
    {
        public static JwtPayload DecodeJwtPayload(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            return jsonToken.Payload;

        }
    }
}
