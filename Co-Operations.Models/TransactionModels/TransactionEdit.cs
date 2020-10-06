using Co_Operations.Models.TransactionProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.TransactionModels
{
    public class TransactionEdit
    {
        public int TransactionID { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationID { get; set; }

        [Required]
        [Display(Name = "Products")]
        public List<TransactionProductEdit> Products { get; set; } = new List<TransactionProductEdit>();

    }
}
