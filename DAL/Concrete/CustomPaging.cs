using DAL.Abstract;
using Domain.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class CustomPaging<T> : ICustomPaging<T>
    {

        public CustomPage<T> GetPagingData(IQueryable<T> orderedQuery, int page, int pageSize, string columnName, string dir)
        {
            int blockSize = 1;

            int reminder = page % blockSize;
            if (reminder != 0)
            {
                page = 1 + page / blockSize;
            }
            else
            {
                page = page / blockSize;
            }

            page = page == 0 ? 1 : page;
            var totalEntities = orderedQuery.Count();


            pageSize = pageSize == 0 ? 10 : pageSize;
            var skip = (page - 1) * pageSize * blockSize;

            var entities = orderedQuery.Skip(skip).Take(pageSize * blockSize);

            return new CustomPage<T> { data = (IOrderedQueryable<T>)entities, totalCount = totalEntities };

        }


    }
    public static class DynamicExtentions
    {
        public static object GetPropertyDynamic<Tobj>(this Tobj self, string propertyName) where Tobj : class
        {
            var param = Expression.Parameter(typeof(Tobj), "value");
            var getter = Expression.Property(param, propertyName);
            var boxer = Expression.TypeAs(getter, typeof(object));
            var getPropValue = Expression.Lambda<Func<Tobj, object>>(boxer, param).Compile();
            return getPropValue(self);
        }
    }
}
