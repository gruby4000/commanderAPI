using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
<<<<<<< HEAD
    public class CommanderContext: DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt): base(opt)
=======
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
>>>>>>> controller
        {

        }

        public DbSet<Command> Commands { get; set; }
<<<<<<< HEAD
    }
}
=======

    }
}
>>>>>>> controller
