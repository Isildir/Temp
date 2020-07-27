namespace EngineerProject.API.Entities.Models
{
    public class UserGroup
    {
        public Group Group { get; set; }

        public int GroupId { get; set; }

        public GroupRelation Relation { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}