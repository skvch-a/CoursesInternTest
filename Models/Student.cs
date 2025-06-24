using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesInternTest.Models;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string FullName { get; set; }

    public Guid CourseId { get; set; }
    
    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; }
}