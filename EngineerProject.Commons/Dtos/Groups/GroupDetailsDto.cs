﻿namespace EngineerProject.Commons.Dtos
{
    public class GroupDetailsDto
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public bool IsOwner { get; set; }

        public bool IsPrivate { get; set; }

        public string Name { get; set; }
    }
}