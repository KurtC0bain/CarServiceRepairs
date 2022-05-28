using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Auto
    {
        [Key]
        public int AutoId { get; set; }
        public string Model { get; set; }
        [MaxLength(17)]
        public string VinCode { get; set; }
        [MaxLength(6)]
        public string TechPass{ get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
    }
}
