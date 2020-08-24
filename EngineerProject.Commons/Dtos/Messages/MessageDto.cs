using System;

namespace EngineerProject.Commons.Dtos
{
    public class MessageDto
    {
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public string Owner { get; set; }
    }
}