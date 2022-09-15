using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBaraholka.DAL.Interfaces;
using TechBaraholka.Domain.Entity;

namespace TechBaraholka.DAL.Repositories
{
    public class CartRepository : IBaseRepository<Cart>
    {
        private readonly ApplicationDbContext _db;
        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Cart entity)
        {
            await _db.Carts.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public Task Delete(Cart entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Cart> GetAll()
        {
            return _db.Carts;
        }

        public async Task<Cart> Update(Cart entity)
        {
            _db.Carts.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
