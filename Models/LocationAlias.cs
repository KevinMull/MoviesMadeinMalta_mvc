using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaltaMoviesMVCcore.Models
{
    public partial class LocationAlias
    {
        public LocationAlias()
        {
            Scenes = new HashSet<Scene>();
        }

        [Key]
        public int LocationAliasId { get; set; }
        [Required]
        public string LocationAliasCountry { get; set; }
        
        public string LocationAliasPlace { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string MapInfoHtml { get; set; }
        //public string LatLong { get; set; }

        public string LatLong
        {
            get
            {
                return Latitude.ToString() + ", " + Longitude.ToString();
            }
        }

        [DisplayName("Movie Location")]
        public string LocationAliasCountryAndPlace
        {
            get
            {
                return LocationAliasCountry.ToString() + ", " + LocationAliasPlace.ToString();
            }
        }

        public ICollection<Scene> Scenes { get; set; }
    }
}
