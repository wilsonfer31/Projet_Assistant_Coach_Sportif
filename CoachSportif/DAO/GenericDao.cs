using CoachSportif.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CoachSportif.DAO
{
    public class GenericDao<T>
    {
        private readonly MyContext db = new MyContext();

        public async Task<List<T>> GetAllAsync()
        {
            return (await db.Set(typeof(T)).ToListAsync()).Cast<T>().ToList();
        }
        public async Task<T> FindByIdAsync(int? id)
        {
            return (T)await db.Set(typeof(T)).FindAsync(id);
        }
        public async Task<bool> UpdateAsync(T o)
        {
            try
            {
                db.Entry(o).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<T> AddAsync(T o)
        {
            try
            {
                o = (T)db.Set(typeof(T)).Add(o);
                await db.SaveChangesAsync();
                return o;
            }
            catch
            {
                return default;
            }
        }
        public async Task<bool> RemoveAsync(T o)
        {
            try
            {
                db.Set(typeof(T)).Remove(o);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}