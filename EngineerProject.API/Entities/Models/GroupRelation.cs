namespace EngineerProject.API.Entities.Models
{
    public enum GroupRelation : byte
    {
        Owner = 1,
        User = 2,
        Invited = 3,
        Requesting = 4,
        Rejected = 5
    }
}