using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly FakeRubikStoreContext _context;
        private readonly DbSet<T> _entities;
        public BaseRepository(FakeRubikStoreContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            var users = _entities.ToList();
            return users;
        }

        public async Task Add(T element)
        {
            await _entities.AddAsync(element);
        }
        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async void Update(int id, T element)
        {
            var toUpdate = await _entities.FindAsync(id);
            //AQUI FALTA CODIGO del autoMapper

        }
        public async Task Delete(int id)
        {
            var forDelete = await _entities.FindAsync(id);
            _entities.Remove(forDelete);
            await _context.SaveChangesAsync();
        }
    }
}
