using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string Breakage { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime WarrantyEnd { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Auto")]
        public int AutoId { get; set; }

        [ForeignKey("Worker")]
        public int WorkerId { get; set; }

        public virtual Auto Auto { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual IEnumerable<DetailOrder> Details { get; set; }
    }
}
