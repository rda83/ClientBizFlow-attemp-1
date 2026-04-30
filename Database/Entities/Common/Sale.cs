using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBizFlow_attemp_1.Database.Entities.Common
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }   // цена на момент продажи

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } // = Quantity * UnitPrice

        public DateTime SaleDate { get; set; }

        [StringLength(200)]
        public string? Note { get; set; }
    }
}
