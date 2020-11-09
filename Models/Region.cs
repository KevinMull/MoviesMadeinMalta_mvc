using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaltaMoviesMVCcore.Models
{
    public partial class Region
    {
        [Key]
        public int RegionId { get; set; }
        [DisplayName("Region")]
        public string RegionName { get; set; }

    }

}
