using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Data
{
    public class LocationUser
    {
        [Key]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Key]
        [ForeignKey(nameof(Location))]
        public int LocationID { get; set; }
        public virtual Location Location { get; set; }

    }
}
