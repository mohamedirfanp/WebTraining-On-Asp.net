using EF_Practise.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Practise.Data
{
    public class StudentDBContext : DbContext
    {

        public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options){ }



        // Link the Table to Model
        public DbSet<StudentModel> Students { get; set; }


    }
}
