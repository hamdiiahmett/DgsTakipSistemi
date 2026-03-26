using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DgsTakipSistemi.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // İleride kendi tablolarımızı buraya DbSet olarak ekleyeceğiz.
    }
}