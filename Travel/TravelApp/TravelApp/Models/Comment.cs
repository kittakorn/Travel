using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Models
{
    class Comment
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentDescription { get; set; }
        public string CommentUserId { get; set; }
        public int CommentPlaceId { get; set; }
    }
}
