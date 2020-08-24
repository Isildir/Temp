using System.Collections.Generic;

namespace EngineerProject.Commons.Dtos
{
    public class GroupAdminDetailsDto
    {
        public GroupAdminDetailsDto()
        {
            Candidates = new List<GroupCandidateDto>();
        }

        public List<GroupCandidateDto> Candidates { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public string Name { get; set; }
    }
}