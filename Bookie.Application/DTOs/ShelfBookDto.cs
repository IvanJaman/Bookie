﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Application.DTOs
{
    public class ShelfBookDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverPhotoUrl { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
