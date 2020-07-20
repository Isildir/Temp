using System;
using System.ComponentModel.DataAnnotations;

namespace EngineerProject.API.Entities.Models
{
    public class File
    {
        public DateTime DateAdded { get; set; }

        [Required, StringLength(100)]
        public string FilePath { get; set; }

        [Key]
        public int Id { get; set; }

        public UserGroup UserGroup { get; set; }

        public int UserGroupId { get; set; }
    }
}