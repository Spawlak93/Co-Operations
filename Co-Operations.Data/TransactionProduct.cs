using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Data
{
    public class TransactionProduct
    {
        [Key]
        [ForeignKey(nameof (Transaction))]
        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }

        [Key]
        [ForeignKey(nameof(Product))]
        public string PruductSKU { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int NumberSold { get; set; }
    }
}
