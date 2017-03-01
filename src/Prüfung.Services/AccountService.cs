using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Prüfung.Common;
using Prüfung.Common.Enums;
using Prüfung.Common.Repository;
using Prüfung.Common.Services;
using Prüfung.Entitys;
using Prüfung.ViewModels;

namespace Prüfung.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public AccountService(IRepository repository, ILogger<AccountService> logger)
        {
            _repository = repository;
        }

        public async Task<UserViewModel> GetUserById(int id)
        {
            var user = await _repository.GetUserById(id);

            if (user != null)
            {
                return _map(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> HasRole(int id, UserRoleType urt)
        {
            var user = await GetUserById(id);

            if (user != null)
            {
                return user.Roles.Any(r => r == (int)urt);
            }
            else
            {
                return false;
            }
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var users = await _repository.GetAllUsers();
            return users.Select(user => _map(user)).ToList();
        }

        public async Task<List<UserViewModel>> DelUser(UserViewModel user)
        {
            _repository.RemoveUser(user.UserId);
            return await GetAllUsers();
        }

        public async Task<List<UserViewModel>> SaveUser(UserViewModel user)
        {
            var usr = await _repository.GetUserById(user.UserId);

            if (usr == null)
            {
                usr = new User();
                usr = _updateUser(usr, user);
            }
            else
            {
                //usr = _delRoles(usr);
                usr = _updateUser(usr, user);
            }

            _repository.SaveChanges();

            return await GetAllUsers();
        }

        public async Task<UserViewModel> GetUserByName(string username)
        {
            var user = await _repository.GetUserByName(username);

            if (user != null)
            {
                return _map(user);
            }
            else
            {
                return null;
            }
        }

        // Privates

        private UserViewModel _map(User user)
        {
            if (user != null)
            {
                return new UserViewModel()
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    ShowName = user.Username,
                    Name = user.Name,
                    Vorname = user.Vorname,
                    Password = user.Password,
                    Roles = _getRoles(user)
                };
            }
            else
            {
                return null;
            }
        }

        private IEnumerable<int> _getRoles(User user)
        {
            var roles = user.RoleToUsers.Select(r => r.Role);
            return roles.Select(r => (int)r.UserRoleType);
        }

        private User _updateUser(User usr, UserViewModel user)
        {
            // Todo wirklich nötig Roles vorher zu löschen!?
            var rtus = _repository.GetRoleToUser(user.UserId);
            _repository.RemoveRange(rtus);

            usr.Name = user.Name;
            usr.Vorname = user.Vorname;
            usr.Username = user.Username;

            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    var rtu = new RoleToUser()
                    {
                        Role = role != -1 ? _repository.GetRole(role) : _repository.GetRole((int)UserRoleType.Default),
                        User = usr
                    };

                    _repository.AddRoleToUser(rtu);
                }
            }

            return usr;
        }

        //private User _delRoles(User user)
        //{
        //    var rtus = _repository.GetRoleToUser(user.UserId);
        //    _repository.RemoveRange(rtus);
        //    return user;
        //}
    }
}
