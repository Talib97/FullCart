using FullCart.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Entities;

public sealed class Role : BaseEntity
{
    public string Name { get; set; }
    public List<UserRole> UserRoles { get; set; }
}
