using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.LocationModels
{
    public class LocationListItem
    {
        [Display(Name ="Location ID")]
        public int ID { get; set; }

        [Display(Name = "Location Name")]
        public string Name { get; set; }

    }
}
