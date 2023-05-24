using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiimpleWebApp.Models;

namespace SiimpleWebApp.DAL
{
    public class SiimpleDbContect:IdentityDbContext<AppUser>
    {
        public SiimpleDbContect(DbContextOptions<SiimpleDbContect> options):base(options)
        {

        }
        public DbSet<UsSection> UsSections { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
