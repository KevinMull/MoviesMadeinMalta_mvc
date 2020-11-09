using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaltaMoviesMVCcore.Models
{
    public class MovieModel
    {
        public string RegionName { get; set; }
        public IEnumerable<MaltaMoviesMVCcore.Models.Movie> Movies { get; set; }
    }
}
