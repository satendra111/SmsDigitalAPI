using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommonEntity
{
    public class CustomPage<T>
    {
        public IOrderedQueryable<T> data { get; set; }
        public int totalCount { get; set; }
    }
}
