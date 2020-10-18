using System.Collections.Generic;

namespace User.Core.Dto
{
    public class PagingDto
    {
        public IEnumerable<UserDto> Result { set; get; }
        
        public int TotalCount { get; set; }
    }
}