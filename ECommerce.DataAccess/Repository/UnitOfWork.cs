using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.DataAccess.Repository.IRepository;

namespace ECommerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}