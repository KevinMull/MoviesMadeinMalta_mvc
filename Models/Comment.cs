using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace MaltaMoviesMVCcore.Models
{
    public partial class Comment
    {
        [Key]
        public int CommentId { get; set; }


        public string Ipaddress { get; set; }
        

        [DisplayName("Name")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        // Set to Now date
        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime CommentDate { get; set; } = DateTime.Now;
    }
}
