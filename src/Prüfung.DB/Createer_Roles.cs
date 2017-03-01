﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prüfung.Common.Enums;
using Prüfung.Entitys;

namespace Prüfung.DB
{
    public static class Createer_Roles
    {
        internal static void Create(DataContext context)
        {
            var urts = (UserRoleType[])Enum.GetValues(typeof(UserRoleType));

            foreach (var urt in urts)
            {
                var r = new Role();
                r.UserRoleType = urt;
                context.Roles.Add(r);
            }

            context.SaveChanges();
        }
    }
}
