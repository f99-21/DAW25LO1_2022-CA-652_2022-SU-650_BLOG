using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;

namespace LO1_2022_CA_652_2022_SU_650.Models
{
    public class blogContext : DbContext
    {

        public blogContext(DbContextOptions<blogContext> options) : base(options) 
        {

        }
    }
}
