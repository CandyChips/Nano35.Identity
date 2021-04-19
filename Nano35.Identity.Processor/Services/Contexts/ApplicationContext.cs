using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.Services.Contexts
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<User> AppUsers { get; set; }
        public DbSet<Role> AppRoles { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            Update();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public void Update()
        {
            Users.Load();
            Roles.Load();
        }
    }
    public class DataInitializer
    {
        public static async Task InitializeRolesAsync(
            RoleManager<Role> roleManager,
            ApplicationContext modelBuilder)
        {
            if(!modelBuilder.AppRoles.Any())
            {
                await roleManager.CreateAsync(new Role("Администратор"));
                await roleManager.CreateAsync(new Role("Приемщик"));
                await roleManager.CreateAsync(new Role("Мастер"));
                await roleManager.CreateAsync(new Role("Менеджер"));

                await modelBuilder.SaveChangesAsync();
            }
        }
    }
}
