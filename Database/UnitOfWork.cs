using System;
using Database.Repositories;

namespace Database
{
    public class UnitOfWork
    {
        private PracticeDbContext _context;

        private UserRepository _userRepository;

        public UserRepository UserRepository
        {
            get
            {
                return _userRepository;
            }
        }

        public UnitOfWork(PracticeDbContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
        }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public void Save()
        {
            try
            {
                BeginTransaction();
                _context.SaveChanges();
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw;
            }
        }
    }
}
