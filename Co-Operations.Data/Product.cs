using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Data
{
    public class Product
    {
        public Product() { }
        public Product(string itemName)
        {
            ItemName = itemName;
            ProductSKU = GenerateSKU();
        }

        [Key]
        public string ProductSKU { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Maker))]
        public string MakerID { get; set; }

        public virtual ApplicationUser Maker { get; set; }

        public virtual ICollection<TransactionProduct> Transactions { get; set; } = new List<TransactionProduct>();


        private string GenerateSKU()
        {
            Random random = new Random();

            var sKU = random.Next(1000).ToString("D3");

            return $"{ItemName.Substring(0, 3).ToUpper()}-{sKU}";
        }
    }
}
