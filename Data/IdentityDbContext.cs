using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace animated_spoon.Models
{

    public class IdentityDbContext : IdentityDbContext<IdentityUser>
    {

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options) { }
    }
}
