using System.ComponentModel.DataAnnotations;

namespace CoursesInternTest.DTO;

public class CreateStudentRequest
{
    [Required]
    public string FullName { get; set; }
}