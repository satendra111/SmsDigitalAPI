using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("CityDetail", Schema = "dbo")]
    internal class CityDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string city { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public double price { get; set; }
        public string status { get; set; }
        public string color { get; set; }
    }
}
