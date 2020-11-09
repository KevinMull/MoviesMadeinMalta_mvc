using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaltaMoviesMVCcore.Models
{
    public partial class LocationSite
    {
        
        [Key]
        public int LocationSiteId { get; set; }

        [DisplayName("Location Site")]
        [Required]
        public string LocationSiteName { get; set; }
        [ForeignKey("LocationPlaceId")]
        public int? LocationPlaceId { get; set; }
      
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

       

        public string LatLong
        {
            get
            {
                return Latitude.ToString() + ", " + Longitude.ToString();
            }
        }
       
        
        public LocationPlace LocationPlace { get; set; }
        public ICollection<Scene> Scenes { get; set; }

        [DisplayName("Location Name")]
        public string LocationPlaceAndSiteName
        //Return a concat of Location place and LocastionSiteName
        //e.g. 'Valleta, Fort St Elmo'.
        //Of Both names the same, i.e. 'Valletta, Valletta' then just return the LocationSiteName only ('Valletta')
        {
            get
            {
                String locationPlaceName = this.LocationPlace.LocationPlaceName;

                if (locationPlaceName == LocationSiteName)

                {
                    return LocationSiteName;
                }
                else
                {
                    return locationPlaceName + ", " + LocationSiteName;
                }
            }
        }
        
        
    }
}
