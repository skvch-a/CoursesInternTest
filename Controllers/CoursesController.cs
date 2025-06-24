using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoursesInternTest.Data;
using CoursesInternTest.DTO;
using CoursesInternTest.Models;

namespace CoursesInternTest.Controllers;

[ApiController]
[Route("[controller]")]
public class CoursesController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await context.Courses
            .Include(c => c.Students)
            .Select(c => new
            {
                c.Id,
                c.Name,
                Students = c.Students.Select(s => new { s.Id, s.FullName }).ToList()
            })
            .ToListAsync();

        return Ok(courses);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
    {
        var course = new Course { Name = request.Name };

        context.Courses.Add(course);
        await context.SaveChangesAsync();

        return Ok(new { id = course.Id });
    }

    [HttpPost("{id:guid}/students")]
    public async Task<IActionResult> AddStudentToCourse(Guid id, [FromBody] CreateStudentRequest request)
    {
        var course = await context.Courses.FindAsync(id);
        if (course == null)
            return NotFound();

        var student = new Student
        {
            FullName = request.FullName,
            CourseId = id
        };

        context.Students.Add(student);
        await context.SaveChangesAsync();

        return Ok(new { id = student.Id });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        var course = await context.Courses.FindAsync(id);
        if (course == null)
            return NotFound();

        context.Courses.Remove(course);
        await context.SaveChangesAsync();

        return NoContent();
    }
}