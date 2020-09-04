namespace EngineerProject.Commons.Dtos
{
    public class UserGroupTileDto : GroupTileDto
    {
        public bool IsOwner { get; set; }

        public bool IsPrivate { get; set; }

        public string Description { get; set; }
    }
}