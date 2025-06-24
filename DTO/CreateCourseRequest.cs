using System.ComponentModel.DataAnnotations;

namespace CoursesInternTest.DTO;

public class CreateCourseRequest
{
    [Required]
    public string Name { get; set; }
}