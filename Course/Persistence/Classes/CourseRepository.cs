using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Course.Core.Dto;
using Course.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Course.Persistence.Classes
{
    public class CourseRepository : BaseRepository<Core.Entity.Course> , ICourseRepository
    {
        private readonly CourseDbContext _context;
      
        public CourseRepository(CourseDbContext context) :base(context)
        {
           _context = context;
      
        }
        
        public void Remove(int id) 
        {
            var course = _context.Courses.Find(id);
            if (course == null)
                throw new Exception("Course Not Found! ");
            _context.Courses.Remove(course);
          
        }
        
        public async Task<Core.Entity.Course> GetById(int courseId)
        {
            var course = await _context.Courses
                .SingleOrDefaultAsync(u => u.CourseId == courseId);
            return course;

        }
        
        public async Task<PagingDto> GetAll(int? pageIndex, int? pageSize)
        {
            PagingDto result = new PagingDto();
          if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageIndex = 10;
            
            result.TotalCount= await _context.Courses.CountAsync();
            var query = _context.Courses.Select(w=>
                new CourseDto
                {
                    CourseName = w.CourseName
                }).Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            
                result.Result =await query.ToListAsync();
                return result;
            
             

        }
    }
}