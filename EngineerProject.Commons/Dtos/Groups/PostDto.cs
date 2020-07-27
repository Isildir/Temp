using System.Collections.Generic;

namespace EngineerProject.Commons.Dtos.Groups
{
    public class PostDto : CommentDto
    {
        public List<CommentDto> Comments { get; set; }

        public bool Pinned { get; set; }

        public string Title { get; set; }
    }
}