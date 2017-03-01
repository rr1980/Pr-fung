﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prüfung.Common;
using Prüfung.Common.Services;
using Prüfung.ViewModels;

namespace Prüfung.Web.ViewComponents
{
    public class NavbarComponent : ViewComponent
    {
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;
        private readonly HttpContext _httpContext;

        public NavbarComponent(IAccountService accountService, IHttpContextAccessor httpContextAccessor, ILogger<NavbarComponent> logger)
        {
            _accountService = accountService;
            _httpContext = httpContextAccessor.HttpContext;
            _logger = logger;
            _logger.LogWarning("NavbarComponent init...");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = Convert.ToInt32(_httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            return View(new NavbarViewModel()
            {
                UserViewModel = (UserViewModel)await _accountService.GetUserById(id)
            });
        }
    }
}
