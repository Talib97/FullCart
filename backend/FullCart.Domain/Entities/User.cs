using FullCart.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Entities;

public class User : BaseEntity
{
   public string Name { get; set; }
   public string Email { get; set; }
   public byte[] PasswordHash { get; set; }
   public byte[] PasswordSalt { get; set; }
   public List<UserRole> UserRoles { get; set; }
   public virtual List<Cart> UserCart { get; set; }
}
