using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.AccountModels
{
    public class CollectCommisionDetail
    {
        [Display(Name = "Amount Owed")]
        public decimal AmountOwed { get; set; } 

        [Display(Name = "Amount Being Collected")]
        public decimal AmountBeingCollected { get; set; }

        [Display(Name = "Date of Collection")]
        public DateTimeOffset DateOfCollection { get; set; }
    }
}
