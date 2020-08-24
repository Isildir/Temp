using System.Collections.Generic;

namespace EngineerProject.Commons.Dtos
{
    public class UserGroupsWrapperDto
    {
        public UserGroupsWrapperDto()
        {
            Participant = new List<UserGroupTileDto>();
            Invited = new List<GroupTileDto>();
            Waiting = new List<GroupTileDto>();
        }

        public List<GroupTileDto> Invited { get; set; }

        public List<UserGroupTileDto> Participant { get; set; }

        public List<GroupTileDto> Waiting { get; set; }
    }
}