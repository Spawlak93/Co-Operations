using Co_Operations.Models.TransactionProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.TransactionModels
{
    public class TransactionCreate
    {
        [Display(Name = "Location")]
        public int LocationID { get; set; }

        public List<TranssactionProductCreate> Products { get; set; }
    }
}
