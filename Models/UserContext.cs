using Microsoft.EntityFrameworkCore;
using System.Linq;
 
namespace dashboard.Models
{
    public class UserContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
	public DbSet<Lender> Lender { get; set; }
	public DbSet<Borrower> Borrower { get; set; }
	public DbSet<Transaction> Transaction { get; set; }
    }
}