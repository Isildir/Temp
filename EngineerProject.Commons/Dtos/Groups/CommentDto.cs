﻿using System;

namespace EngineerProject.Commons.Dtos.Groups
{
    public class CommentDto
    {
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? EditDate { get; set; }

        public bool? Edited { get; set; }

        public int Id { get; set; }

        public bool IsOwner { get; set; }
    }
}