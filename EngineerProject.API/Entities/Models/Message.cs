﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EngineerProject.API.Entities.Models
{
    public class Message
    {
        [Required]
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }

        [Key]
        public int Id { get; set; }

        public User Sender { get; set; }

        public int SenderId { get; set; }
    }
}