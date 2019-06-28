using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Models
{
    class Comment
    {
        public int CommentId { get; set; }
        public string CommentDescription { get; set; }
        public System.DateTime CommentDate { get; set; }
        public int CommentPlaceId { get; set; }
        public string CommentUserId { get; set; }

        public virtual Place Place { get; set; }
    }
}
