using System;

namespace EngineerProject.Commons.Dtos
{
    public class FileDto
    {
        public DateTime DateAdded { get; set; }

        public string FileName { get; set; }

        public int Id { get; set; }

        public bool IsOwner { get; set; }

        public string Owner { get; set; }

        public string Size { get; set; }
    }
}