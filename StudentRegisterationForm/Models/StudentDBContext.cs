using Microsoft.EntityFrameworkCore;

namespace StudentRegisterationForm.Models
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions option) : base(option)
        {
        }

        public DbSet<StudentModel> StudentsDataTab { get; set; }
      
    }
}