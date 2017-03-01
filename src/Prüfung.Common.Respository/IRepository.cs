using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prüfung.Entitys;

namespace Prüfung.Common.Repository
{
    public interface IRepository
    {
        Task<User> GetUserById(int id);
        void AddRoleToUser(RoleToUser rtu);
        IEnumerable<RoleToUser> GetRoleToUser(int userId);
        void RemoveRange(IEnumerable<IEntity> rtus);
        Role GetRole(int role);
        Task<IEnumerable<User>> GetAllUsers();
        void RemoveUser(int id);
        void SaveChanges();
        Task<User> GetUserByName(string username);
    }
}
