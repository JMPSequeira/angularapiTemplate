using Microsoft.EntityFrameworkCore;

namespace angularapiTemplate.Data
{
    public class angularapiTemplateContext : DbContext
    {
##if (Sqlite == true)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=angularapiTemplate.db");
            base.OnConfiguring(optionsBuilder);
        }
##endif    
    }
}