using System.ComponentModel.DataAnnotations;

namespace Pharmacie.Models
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
    }
}
