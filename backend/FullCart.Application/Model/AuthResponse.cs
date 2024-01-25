using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Model
{
    public record AuthResponse(string AccessToken, AuthUser UserInfo);
    public record AuthUser(string id,string name, List<string> roles);
}
