using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prüfung.Common;
using Prüfung.Common.Repository;
using Prüfung.Entitys;

namespace Prüfung.DB
{
    public class Repository : IRepository
    {
        private readonly DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddRoleToUser(RoleToUser rtu)
        {
            _dataContext.RoleToUsers.Add(rtu);
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _dataContext.Users.Include(u => u.RoleToUsers).ThenInclude(r => r.Role).ToListAsync();
        }

        public Role GetRole(int role)
        {
            return _dataContext.Roles.FirstOrDefault(r => (int)r.UserRoleType == role);
        }

        public IEnumerable<RoleToUser> GetRoleToUser(int userId)
        {
            return _dataContext.RoleToUsers.Where(rtu => rtu.UserId == userId);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dataContext.Users.Include(u => u.RoleToUsers).ThenInclude(r => r.Role).SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetUserByName(string username)
        {
            return await _dataContext.Users.Include(u => u.RoleToUsers).ThenInclude(r => r.Role).SingleOrDefaultAsync(u => u.Username == username);
        }

        public void RemoveUser(int id)
        {
            var item = (User)_dataContext.Find(typeof(User), id);
            _dataContext.Remove<User>(item);
            _dataContext.SaveChanges();
        }

        public void RemoveRange(IEnumerable<IEntity> rtus)
        {
            _dataContext.RemoveRange(rtus);
            _dataContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }
    }
}
