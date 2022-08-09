using Domain.CommonEntity;

namespace API.Model
{
    public class CitySearchModel: Pagination
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
