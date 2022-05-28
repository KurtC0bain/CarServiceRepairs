using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Detail> Details { get; set; }
    }
}