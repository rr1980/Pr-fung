using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prüfung.Common.Enums;
using Prüfung.Entitys;
using Prüfung.ViewModels;

namespace Prüfung.Common.Services
{
    public interface IAccountService
    {
        //Task<IEntity> GetUserById(int id);

        Task<bool> HasRole(int id, UserRoleType urt);
        Task<List<UserViewModel>> GetAllUsers();
        Task<List<UserViewModel>> DelUser(UserViewModel user);
        Task<List<UserViewModel>> SaveUser(UserViewModel user);
        Task<UserViewModel> GetUserById(int id);
        Task<UserViewModel> GetUserByName(string username);
    }
}
