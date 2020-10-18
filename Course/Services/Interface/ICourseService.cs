using System.Threading.Tasks;
using Course.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Interface
{
    public interface ICourseService
    {
        Task<CourseDto> AddCourse([FromBody] CourseDto courseDto);
        Task<string> RemoveCourse(int courseId);
        Task<CourseDto> GetById(int courseId);
        Task<PagingDto> GetAll([FromQuery] int? pageIndex, [FromQuery] int? pageSize);
        
        Task<CourseDto> UpdateCourse(int id,  CourseDto courseDto);
    }
    
   
}