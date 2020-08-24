using System;
using System.Collections.Generic;

namespace EngineerProject.Commons.Dtos
{
    public class PostDto : CommentDto
    {
        public List<CommentDto> Comments { get; set; }

        public DateTime? EditDate { get; set; }
    }
}