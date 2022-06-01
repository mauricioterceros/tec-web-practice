using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class UserRepository
    {
        private PracticeDbContext _context;

        public UserRepository(PracticeDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public User CreateUser(User user)
        {
            _context.Set<User>().Add(user);
            return user;
        }

        public User GetById(Guid id)
        {
            return _context.Set<User>().Find(id);
        }

        public User UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return user;
        }

        public User DeleteUser(User user)
        {
            _context.Set<User>().Remove(user);
            return user;
        }
    }
}
