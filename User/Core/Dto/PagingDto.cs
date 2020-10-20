using System.Collections.Generic;

namespace User.Core.Dto
{
    public class PagingDto<T>
    {
        public int TotalCount { set; get; }
        public IList<T> Result { set; get; }
    }
}