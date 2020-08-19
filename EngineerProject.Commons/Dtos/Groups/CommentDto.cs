using System;

namespace EngineerProject.Commons.Dtos.Groups
{
    public class CommentDto
    {
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public int Id { get; set; }

        public bool IsOwner { get; set; }

        public string Owner { get; set; }
    }
}