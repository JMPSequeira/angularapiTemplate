using Microsoft.EntityFrameworkCore;

namespace angularapiTemplate.Data
{
    public class angularapiTemplateContext : DbContext
    {
        public angularapiTemplateContext(DbContextOptions<angularapiTemplateContext> options) : base(options)
        {
##if (Sqlite == true)
            options.UseSqlite("Data Source=angularapiTemplate.db");
##endif
        }

        public angularapiTemplateContext() { }
    }
}