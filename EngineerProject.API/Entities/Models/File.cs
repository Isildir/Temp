using EngineerProject.API.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EngineerProject.API.Entities.Models
{
    public class File
    {
        [Required]
        public DateTime DateAdded { get; set; }

        [Required, StringLength(50)]
        public string FileName { get; set; }

        [Required, StringLength(100)]
        public string FilePath { get; set; }

        public FileType FileType { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }

        [Key]
        public int Id { get; set; }

        public string Size { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}