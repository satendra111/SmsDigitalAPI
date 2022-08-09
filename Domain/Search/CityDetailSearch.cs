using Domain.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Search
{
    public class CityDetailSearch: Pagination
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
