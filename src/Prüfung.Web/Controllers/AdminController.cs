using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Prüfung.Common.Services;
using Prüfung.ViewModels;

namespace Prüfung.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger _logger;
        private readonly IAccountService _accountService;

        public AdminController(ILogger<AdminController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _accountService.GetAllUsers();
            result.Insert(0, new UserViewModel()
            {
                UserId = -1,
                ShowName = "Neu...",
                Roles = new int[] { -1 }
            });

            return View(new AdminViewModel()
            {
                Users = result
            });
        }

        public async Task<AdminViewModel> SaveUser(UserViewModel user)
        {
            List<UserViewModel> result;

            if (!ModelState.IsValid)
            {
                result = await _accountService.GetAllUsers();
                result.Insert(0, new UserViewModel()
                {
                    UserId = -1,
                    ShowName = "Neu...",
                    Roles = new int[] { -1 }
                });

                return new AdminViewModel()
                {
                    Users = result,
                    Errors = GetModelStateErrors(ModelState)
                };
            }

            result = await _accountService.SaveUser(user);
            result.Insert(0, new UserViewModel()
            {
                UserId = -1,
                ShowName = "Neu...",
                Roles = new int[] { -1 }
            });

            return new AdminViewModel()
            {
                Users = result,
            };
        }

        public async Task<AdminViewModel> DelUser(UserViewModel user)
        {
            var result = await _accountService.DelUser(user);
            result.Insert(0, new UserViewModel()
            {
                UserId = -1,
                ShowName = "Neu...",
                Roles = new int[] { -1 }
            });

            return new AdminViewModel()
            {
                Users = result,
            };
        }

        public IActionResult Error()
        {
            return View();
        }


        public List<string> GetModelStateErrors(ModelStateDictionary ModelState)
        {
            List<string> errorMessages = new List<string>();

            var validationErrors = ModelState.Values.Select(x => x.Errors);
            validationErrors.ToList().ForEach(ve =>
            {
                var errorStrings = ve.Select(x => x.ErrorMessage);
                errorStrings.ToList().ForEach(em =>
                {
                    errorMessages.Add(em);
                });
            });

            return errorMessages;
        }
    }
}



//_logger.LogWarning("loulou");
//_logger.LogError("loulou");
//_logger.LogWarning(LoggingEvents.GET_ITEM, "Getting item {ID}", 1);