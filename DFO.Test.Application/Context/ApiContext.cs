using Microsoft.EntityFrameworkCore;
using DFO.Test.Application.Model.Entities;

namespace DFO.Test.Application.Context
{
    public class ApiContext : DbContext
    {

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
