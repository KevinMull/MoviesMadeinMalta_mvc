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
        public int? TitleId { get; set; } // foreign key from Movie
        [ForeignKey("LocationSiteId")]
        public int? LocationSiteId { get; set; }
        [ForeignKey("LocationAliasId")]
        public int? LocationAliasId { get; set; }
        public string Notes { get; set; }
        public int? SceneOrder { get; set; }
        public string TitleList { get; set; }

        public LocationAlias LocationAlias { get; set; }
        public LocationSite LocationSite { get; set; }
        [ForeignKey("TitleId")]
        public virtual Movie Movie { get; set; }
    }
}
