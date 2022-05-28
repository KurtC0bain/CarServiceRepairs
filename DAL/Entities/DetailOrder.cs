using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DetailOrder
    {
        [Key]
        public int DetailId { get; set; }
        [Key]
        public int OrderId { get; set; }
        public int Amount { get; set; }

        public Detail Detail { get; set; }
        public Order Order { get; set; }    

    }
}
