using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Repository.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int ClientId { get; set; }
        public int EntryUserId { get; set; }
       
        [Column(TypeName = "datetime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime EntryDate { get; set; } = DateTime.Now;
        [Required]
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
