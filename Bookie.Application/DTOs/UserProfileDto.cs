﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Application.DTOs
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string RoleName { get; set; }
        public List<ShelfDto> Shelves { get; set; } = new();
        public List<ReviewDto> Reviews { get; set; } = new();
    }
}
