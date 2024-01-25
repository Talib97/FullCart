using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Model
{
    public record CategoryDto (
        Guid Id,
        string Name
    );
}
