using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.Core.Dto;
using Course.Persistence.Interfaces;
using Course.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Service
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      

        public CourseService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        public async Task<CourseDto> AddCourse([FromBody] CourseDto courseDto)
        {
            var course = _mapper.Map<CourseDto, Core.Entity.Course>(courseDto);
            _unitOfWork.CourseRepository.Add(course);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Core.Entity.Course, CourseDto>(course);
            result.CourseName = course.CourseName;
            return result;
        }
        
        public async Task<string> RemoveCourse(int courseId)
        {
            _unitOfWork.CourseRepository.Remove(courseId);
            await _unitOfWork.CompleteAsync();
            return "accept deleted";

        }
        
        public async Task<CourseDto> GetById(int courseId)
        {
            var course = await _unitOfWork.CourseRepository.GetById(courseId);
            var p = _mapper.Map<Core.Entity.Course, CourseDto>(course);
            p.CourseName = course.CourseName;
            return p;
        }
        
        public async Task<PagingDto> GetAll([FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            
            var course = await _unitOfWork.CourseRepository.GetAll(pageIndex, pageSize);
            return course;
        }
        public async Task<CourseDto> UpdateCourse(int id, CourseDto courseDto)
        {
            var course = await _unitOfWork.CourseRepository.GetById(id);
            if (course == null)
                throw new Exception("Not Found... ");
            var courses = _mapper.Map(courseDto, course);
            _unitOfWork.CourseRepository.Update(courses);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Core.Entity.Course, CourseDto>(courses);
        }

    }
    
}