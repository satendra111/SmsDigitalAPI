using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommonEntity
{
    public class DataResult<T>
    {
        public List<T> list { get; set; }
        public int totalCount { get; set; }
    }
}
