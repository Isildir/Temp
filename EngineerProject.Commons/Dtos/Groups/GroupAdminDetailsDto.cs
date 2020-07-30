using System.Collections.Generic;

namespace EngineerProject.Commons.Dtos.Groups
{
    public class GroupAdminDetailsDto
    {
        public GroupAdminDetailsDto()
        {
            Candidates = new List<GroupCandidateDto>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public List<GroupCandidateDto> Candidates { get; set; }
    }
}