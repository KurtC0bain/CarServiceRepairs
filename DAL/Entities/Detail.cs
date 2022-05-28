using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Detail
    {
        [Key]
        public int DetailId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        [ForeignKey("Unit")]
        public int UnitId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual IEnumerable<DetailOrder> Orders { get; set; }
    }
}
