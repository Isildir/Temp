using System;
using System.ComponentModel.DataAnnotations;

namespace EngineerProject.API.Entities.Models
{
    public class Comment
    {
        [Required, StringLength(500)]
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? EditDate { get; set; }

        public bool? Edited { get; set; }

        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public Post UserPost { get; set; }

        public int UserPostId { get; set; }
    }
}