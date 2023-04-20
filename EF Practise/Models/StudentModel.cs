using System.ComponentModel.DataAnnotations;

namespace EF_Practise.Models
{
    public class StudentModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string? RollNo { get; set; }
        public string? Name { get; set; }

        public string? Dept { get; set; }

        public DateTime DOB { get; set; }
    }
}
