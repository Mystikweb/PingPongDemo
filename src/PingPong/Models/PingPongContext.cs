using Microsoft.EntityFrameworkCore;

namespace PingPong.Models
{
    public class PingPongContext : DbContext
    {
        public PingPongContext(DbContextOptions<PingPongContext> options)
            : base(options) { }

        public DbSet<Player> Players { get; set; }
    }
}