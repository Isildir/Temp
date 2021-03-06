﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EngineerProject.API.Entities.Models
{
    public class Group
    {
        public List<Message> ChatContent { get; set; }

        public string Description { get; set; }

        public List<File> Files { get; set; }

        [Key]
        public int Id { get; set; }

        public bool IsPrivate { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public List<Post> Posts { get; set; }

        public List<UserGroup> Users { get; set; }
    }
}