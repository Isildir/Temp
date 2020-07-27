using System.Collections.Generic;

namespace EngineerProject.Commons.Dtos.Groups
{
    public class UserGroupsWrapperDto
    {
        public UserGroupsWrapperDto()
        {
            Participant = new List<GroupTileDto>();
            Invited = new List<GroupTileDto>();
            Waiting = new List<GroupTileDto>();
        }

        public List<GroupTileDto> Invited { get; set; }

        public List<GroupTileDto> Participant { get; set; }

        public List<GroupTileDto> Waiting { get; set; }
    }
}