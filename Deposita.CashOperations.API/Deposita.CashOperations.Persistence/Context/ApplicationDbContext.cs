
using Deposita.CashOperations.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Deposita.CashOperations.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Transaction> Transactions => Set<Transaction>();
    }
}
