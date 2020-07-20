using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EngineerProject.API.Entities.Models
{
    public class UserGroup
    {
        public List<File> Files { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }

        [Key]
        public int Id { get; set; }

        public List<Post> Posts { get; set; }

        public GroupRelation Relation { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}