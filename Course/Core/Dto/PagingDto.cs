using System.Collections.Generic;

namespace Course.Core.Dto
{
    public class PagingDto
    {
        public IEnumerable<CourseDto> Result { set; get; }
        
        public int TotalCount { get; set; }
    }
}