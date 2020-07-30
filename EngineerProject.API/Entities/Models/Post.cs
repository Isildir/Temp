using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EngineerProject.API.Entities.Models
{
    public class Post
    {
        public List<Comment> Comments { get; set; }

        [Required, StringLength(500)]
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? EditDate { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }

        [Key]
        public int Id { get; set; }

        public bool Pinned { get; set; }

        public string Title { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}