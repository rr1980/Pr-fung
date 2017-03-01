using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Prüfung.Common.Enums;

namespace Prüfung.Web.Authentication
{
    public class AuthPolicyRequirement : IAuthorizationRequirement
    {
        public UserRoleType UserRoleType;

        public AuthPolicyRequirement(UserRoleType type)
        {
            this.UserRoleType = type;
        }
    }
}
