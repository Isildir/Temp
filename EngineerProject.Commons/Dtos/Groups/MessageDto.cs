using System;

namespace EngineerProject.Commons.Dtos.Groups
{
    public class MessageDto
    {
        public string Owner { get; set; }

        public string Content { get; set; }

        public DateTime DateAdded { get; set; }
    }
}