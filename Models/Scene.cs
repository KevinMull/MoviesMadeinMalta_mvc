using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaltaMoviesMVCcore.Models
{
    public partial class Scene
    {
        [Key]
        public int SceneId { get; set; }
        [ForeignKey("TitleId")]
        public int? TitleId { get; set; } // foreign key from Movie
        public int? LocationSiteId { get; set; }
        public int? LocationAliasId { get; set; }
        public string Notes { get; set; }
        public int? SceneOrder { get; set; }
        public string TitleList { get; set; }

        public LocationAlias LocationAlias { get; set; }
        public LocationSite LocationSite { get; set; }
        public Movie Movie { get; set; }
    }
}
