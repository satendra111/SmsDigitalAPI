using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommonEntity
{
    public class Pagination
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string ColumnName { get; set; }
        public string Direction { get; set; }
    }
}
