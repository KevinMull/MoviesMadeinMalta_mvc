using System;
using System.Collections.Generic;

namespace MaltaMoviesMVCcore.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Ipaddress { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Comments1 { get; set; }
        public DateTime? CommentDate { get; set; }
    }
}
