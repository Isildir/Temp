﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EngineerProject.Commons.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }

        public string Email { get; set; }
    }
}