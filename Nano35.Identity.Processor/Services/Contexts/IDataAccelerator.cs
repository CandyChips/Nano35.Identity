using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Nano35.Identity.Processor.Services.Contexts
{
    public interface IDataAccelerator
    {
        ApplicationContext Context {get;}
        IDbContextTransaction BeginTransaction();
        void SaveChanges();
        Task SaveChangesAsync();
    }
    public class EntityFrameworkAccelerator : IDataAccelerator
    {
        private ApplicationContext _context;
        
        public ApplicationContext Context { get => _context; protected set => _context = value; }

        public EntityFrameworkAccelerator(ApplicationContext context)
        {
            Context = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
