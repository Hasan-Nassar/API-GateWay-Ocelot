using System.Threading.Tasks;
using Course.Core.Dto;
using Course.Services.Interface;
using Microsoft.AspNetCore.Mvc;


namespace Course.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        
        private readonly ICourseService _courseService;
        public CourseController( ICourseService courseService )
        {
            _courseService = courseService;
        }
        /// <summary>
        /// Add Course
        /// </summary>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPost("Add Course")]
        public async Task<IActionResult> AddCourse([FromBody] CourseDto courseDto) => Ok(await _courseService.AddCourse(courseDto));
        /// <summary>
        /// Delete Course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpDelete("Delete course")]
        public async Task<IActionResult> RemoveCourse(int courseId) => Ok(await _courseService.RemoveCourse(courseId));
        /// <summary>
        /// Get Course 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("GetCourseById")]
        public async Task<IActionResult> GetCourse(int courseId) => Ok(await _courseService.GetById(courseId));
        /// <summary>
        /// Get All Course
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAll([FromQuery] int? pageIndex, [FromQuery] int? pageSize) => Ok(await _courseService.GetAll(pageIndex, pageSize));
        /// <summary>
        /// EditCourse
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPut("{CourseId}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] CourseDto courseDto) => Ok(await _courseService.UpdateCourse(id, courseDto));
        
    }
}