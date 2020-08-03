using System;

namespace EngineerProject.Commons.Dtos.Groups
{
    public class FileDto
    {
        public int Id { get; set; }

        public string Owner { get; set; }

        public DateTime DateAdded { get; set; }

        public string FileName { get; set; }

        public bool IsOwner { get; set; }

        public string Size { get; set; }
    }
}