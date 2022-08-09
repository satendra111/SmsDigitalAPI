using Domain.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICustomPaging<T>
    {
        CustomPage<T> GetPagingData(IQueryable<T> orderedQuery, int page, int pageSize, string columnName, string dir);
    }
}
