using System;
using System.ComponentModel.DataAnnotations;

namespace EngineerProject.API.Entities.Models
{
    public class Message
    {
        [Required, StringLength(100)]
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        [Key]
        public int Id { get; set; }

        public User Sender { get; set; }

        public int SenderId { get; set; }
    }
}