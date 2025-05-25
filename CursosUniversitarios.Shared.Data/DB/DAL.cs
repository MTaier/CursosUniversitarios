using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosUniversitarios.Shared.Data.DB
{
    public class DAL<T> where T : class
    {
        private readonly CursosUniversitariosContext context;

        public DAL()
        {
            context = new CursosUniversitariosContext();
        }

        public void Create(T value)
        {
            context.Set<T>().Add(value);
            context.SaveChanges();
        }

        public IEnumerable<T> Read()
        {
            return context.Set<T>().ToList();
        }

        public T? ReadBy(Func<T, bool> predicate)
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        public void Update(T value)
        {
            context.Set<T>().Update(value);
            context.SaveChanges();
        }

        public void Delete(T value)
        {
            context.Set<T>().Remove(value);
            context.SaveChanges();
        }
    }
}
