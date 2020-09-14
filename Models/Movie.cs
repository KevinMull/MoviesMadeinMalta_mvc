using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaltaMoviesMVCcore.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Scenes = new HashSet<Scene>();
        }

        [Key]
        public int TitleId { get; set; }
        [Required]
        public string Title { get; set; }
        [DisplayName("Title")]
        public string ParsedTitle { get; set; }
        [DisplayName("Year")]
        public int? TitleYear { get; set; }
        //public string TitleAndYear { get; set; } // see  public override string ToString()
       
        public string ImdbId { get; set; }
        public string ImdbUrl
        {
            get
            {
                return "https://www.imdb.com/title/" + ImdbId;
            }
        }
        public string Summary { get; set; }
        //public bool? NewTitle { get; set; }
        public bool? ExcludeTitle { get; set; }
        public bool? Widescreen { get; set; }


         [DisplayName("Title")]
        public override string ToString() 
        {
            return Title + " (" + TitleYear + ")";
            //get
            //{
            //    return Title + " (" + TitleYear + ")";
            //}
        }



       public ICollection<Scene> Scenes { get; set; }
        
    }
}
