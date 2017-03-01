using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prüfung.Common;
using Prüfung.Common.Enums;

namespace Prüfung.Entitys
{
    public class Role : IEntity
    {
        public int RoleId { get; set; }
        public UserRoleType UserRoleType { get; set; }
        public virtual ICollection<RoleToUser> RoleToUsers { get; set; }
    }
}
