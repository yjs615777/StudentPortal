using Microsoft.EntityFrameworkCore;
using StudentPortal.Entities;

namespace StudentPortal.Data
{
    public class SchoolContext(DbContextOptions<SchoolContext> options) : DbContext(options)
    {
        public DbSet<Student> Students => Set<Student>();
    }
}
