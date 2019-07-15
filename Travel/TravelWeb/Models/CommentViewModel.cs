using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWeb.Models
{
    public class CommentViewModel
    {
        public CommentViewModel(Comment comment)
        {
            CommentDate = comment.CommentDate;
            CommentDescription = comment.CommentDescription;
            CommentId = comment.CommentId;
            Name = comment.AspNetUser.Name;
            CommentUserId = comment.CommentUserId;
            CommentPlaceId = comment.CommentPlaceId;
        }
        public int CommentId { get; set; }
        public string Name { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentDescription { get; set; }
        public string CommentUserId { get; set; }
        public int CommentPlaceId { get; set; }
    }
}