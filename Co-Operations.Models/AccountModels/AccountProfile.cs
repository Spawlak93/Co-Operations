using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.AccountModels
{
    public class AccountProfile
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Display(Name = "Funds Earned")]
        public decimal FundsEarned { get; set; }

        [Display(Name = "Funds Payed Out")]
        public decimal FundsPayedOut { get; set; }

        [Display(Name = "Funds Owed")]
        public decimal FundsOwed { get; set; }

        
    }
}
