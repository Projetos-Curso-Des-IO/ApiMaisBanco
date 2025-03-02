using ApiFuncional.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiFuncional.Data
{
	public class ApiDbContext : IdentityDbContext
	{
		public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) {}
		public DbSet<Produto> Produtos { get; set; }
		public DbSet<ClienteFornec> Clientes { get; set; }
	}
}
