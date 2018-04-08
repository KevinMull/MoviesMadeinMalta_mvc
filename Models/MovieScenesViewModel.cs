using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MaltaMoviesMVCcore.Models
{
    public class MovieScenesViewModel
    {

        public MovieScenesViewModel()
        {

        }
        public int TitleId { get; }
        public string Title { get; set; }

        [DisplayName("Year")]
        public int? TitleYear { get; set; }
        public string Summary { get; set; }

        public ICollection<Scene> Scenes
        {
            get; set;

        }
    }
}
