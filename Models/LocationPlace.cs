using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaltaMoviesMVCcore.Models
{
    public partial class LocationPlace
    {
        public LocationPlace()
        {
            LocationSites = new HashSet<LocationSite>();
        }

        [Key]
        public int LocationPlaceId { get; set; }

        [DisplayName("Location Place")]
        [Required]
        public string LocationPlaceName { get; set; }

        public int RegionId { get; set; }

        public string RegionName { get; set; }

      //  public LocationSite LocationSite { get; set; }


        public ICollection<LocationSite> LocationSites { get; set; }
    }
}
