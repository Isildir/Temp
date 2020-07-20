using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EngineerProject.API.Entities.Models
{
    public class Post
    {
        public List<Comment> Comments { get; set; }

        [Key]
        public int Id { get; set; }

        public UserGroup UserGroup { get; set; }

        public int UserGroupId { get; set; }
    }
}